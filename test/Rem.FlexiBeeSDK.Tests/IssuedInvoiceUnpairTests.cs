using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients.Banks;
using Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model.Response;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class IssuedInvoiceUnpairTests
{
    private readonly Mock<IBankClient> _bankClientMock = new();
    private readonly Mock<HttpMessageHandler> _httpHandlerMock = new();
    private readonly IssuedInvoiceClient _client;

    public IssuedInvoiceUnpairTests()
    {
        var settings = new FlexiBeeSettings { Server = "https://test.flexibee.eu/v2", Company = "test" };

        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory
            .Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(() => new HttpClient(_httpHandlerMock.Object));

        var resultHandler = new Mock<IResultHandler>();

        _client = new IssuedInvoiceClient(
            settings,
            httpClientFactory.Object,
            resultHandler.Object,
            NullLogger<IssuedInvoiceClient>.Instance,
            _bankClientMock.Object
        );
    }

    [Fact]
    public async Task SaveAsync_UnpairIfNecessary_UnpairsPairedPayments()
    {
        // GET response: invoice with a paired bank payment
        // Use Dictionary for JSON property names containing '@'
        var vazba = new Dictionary<string, object>
        {
            ["id"] = 629476,
            ["typVazbyK"] = "typVazbyDokl.uhrada",
            ["castka"] = 1379.0,
            ["storno"] = false,
            ["b"] = "90313",
            ["b@internalId"] = 90313,
            ["b@evidencePath"] = "banka"
        };

        var getResponse = new Dictionary<string, object>
        {
            ["winstrom"] = new Dictionary<string, object>
            {
                ["faktura-vydana"] = new[]
                {
                    new Dictionary<string, object>
                    {
                        ["id"] = "83214",
                        ["kod"] = "INV-001",
                        ["vazby"] = new[] { vazba }
                    }
                }
            }
        };

        // POST response: success
        var postResponse = new
        {
            winstrom = new
            {
                success = "true",
                stats = new { created = "0", updated = "1", deleted = "0", skipped = "0", failed = "0" },
                results = new[] { new { id = 83214 } }
            }
        };

        SetupHttpResponse(HttpMethod.Get, getResponse);
        SetupHttpResponse(HttpMethod.Post, postResponse);

        _bankClientMock
            .Setup(b => b.UnPairPayment(90313, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new OperationResult<OperationResultDetail>(HttpStatusCode.OK));

        var invoice = new Model.Invoices.IssuedInvoiceDetailFlexiDto { Code = "INV-001" };

        await _client.SaveAsync(invoice, unpairIfNecessary: true);

        _bankClientMock.Verify(b => b.UnPairPayment(90313, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SaveAsync_UnpairIfNecessaryFalse_DoesNotUnpair()
    {
        var postResponse = new
        {
            winstrom = new
            {
                success = "true",
                stats = new { created = "1", updated = "0", deleted = "0", skipped = "0", failed = "0" },
                results = new[] { new { id = 83214 } }
            }
        };

        SetupHttpResponse(HttpMethod.Post, postResponse);

        var invoice = new Model.Invoices.IssuedInvoiceDetailFlexiDto { Code = "INV-001" };

        await _client.SaveAsync(invoice, unpairIfNecessary: false);

        _bankClientMock.Verify(b => b.UnPairPayment(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SaveAsync_UnpairIfNecessary_InvoiceNotFound_StillSaves()
    {
        // GET returns empty array (invoice not found)
        var getResponse = new
        {
            winstrom = new Dictionary<string, object>
            {
                ["faktura-vydana"] = new object[0]
            }
        };

        var postResponse = new
        {
            winstrom = new
            {
                success = "true",
                stats = new { created = "1", updated = "0", deleted = "0", skipped = "0", failed = "0" },
                results = new[] { new { id = 83215 } }
            }
        };

        SetupHttpResponse(HttpMethod.Get, getResponse);
        SetupHttpResponse(HttpMethod.Post, postResponse);

        var invoice = new Model.Invoices.IssuedInvoiceDetailFlexiDto { Code = "INV-NEW" };

        var result = await _client.SaveAsync(invoice, unpairIfNecessary: true);

        _bankClientMock.Verify(b => b.UnPairPayment(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        Assert.True(result.IsSuccess);
    }

    private void SetupHttpResponse(HttpMethod method, object responseBody)
    {
        _httpHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == method),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseBody))
            });
    }
}
