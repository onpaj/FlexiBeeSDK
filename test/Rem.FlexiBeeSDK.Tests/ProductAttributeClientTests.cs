using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients.Products.Attributes;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model.Products.Attributes;
using Xunit;
using static Rem.FlexiBeeSDK.Tests.StubHttpMessageHandler;

namespace Rem.FlexiBeeSDK.Tests;

public class ProductAttributeClientTests
{
    private const int TopProductTypeId = 90;
    private const string BooleanTypeKey = "typAtribut.boolean";
    private const string OkWrite = "{\"winstrom\":{\"success\":\"true\",\"results\":[{\"id\":\"1\"}]}}";

    private readonly ProductReference _product = ProductReference.FromCode("ABC001");

    private static string Rows(params string[] rows) => $"{{\"winstrom\":{{\"atribut\":[{string.Join(",", rows)}]}}}}";
    private static string Row(int id, string valBoolean = "true") => $"{{\"id\":\"{id}\",\"valBoolean\":\"{valBoolean}\",\"cenik\":\"code:ABC001\",\"typAtributu\":\"code:TOPPRODUKT\"}}";
    private static string TypeRow(string typeKey) => $"{{\"winstrom\":{{\"typ-atributu\":[{{\"id\":\"90\",\"kod\":\"TOPPRODUKT\",\"typAtributK\":\"{typeKey}\"}}]}}}}";

    private static (ProductAttributeClient Client, StubHttpMessageHandler Handler) CreateClient(Func<RecordedRequest, HttpResponseMessage> respond)
    {
        var handler = new StubHttpMessageHandler(respond);
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(() => new HttpClient(handler));

        var client = new ProductAttributeClient(
            new FlexiBeeSettings { Server = "https://test.flexibee.eu", Company = "test", Login = "l", Password = "p" },
            factory.Object,
            new Mock<IResultHandler>().Object,
            NullLogger<ProductAttributeClient>.Instance);

        return (client, handler);
    }

    /// <summary>Routes GETs by evidence and answers every POST with success.</summary>
    private static Func<RecordedRequest, HttpResponseMessage> Route(string typeKey, string rowsJson) => r =>
    {
        if (r.Method == HttpMethod.Post) return Json(HttpStatusCode.OK, OkWrite);
        return r.Url.Contains("/typ-atributu/") ? Json(HttpStatusCode.OK, TypeRow(typeKey)) : Json(HttpStatusCode.OK, rowsJson);
    };

    [Fact]
    public async Task GetAsync_FiltersInUrlPathByProductCodeAndAttributeType()
    {
        var (client, handler) = CreateClient(_ => Json(HttpStatusCode.OK, Rows(Row(5))));

        await client.GetAsync(_product, TopProductTypeId);

        var url = handler.Requests.Single().Url;
        url.Should().StartWith("https://test.flexibee.eu/c/test/atribut/(cenik='code%3AABC001'%20and%20typAtributu=90).json?");
        url.Should().NotContain("filter=");
    }

    [Fact]
    public async Task GetAsync_UsesNumericOperandForProductId()
    {
        var (client, handler) = CreateClient(_ => Json(HttpStatusCode.OK, Rows()));

        await client.GetAsync(ProductReference.FromId(1033), TopProductTypeId);

        handler.Requests.Single().Url.Should().Contain("/atribut/(cenik=1033%20and%20typAtributu=90).json");
    }

    [Fact]
    public async Task GetAsync_ReturnsParsedValue_WhenRowExists()
    {
        var (client, _) = CreateClient(_ => Json(HttpStatusCode.OK, Rows(Row(5))));

        var result = await client.GetAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeTrue();
        result.Result!.Id.Should().Be(5);
        result.Result.BooleanValue.Should().BeTrue();
    }

    [Fact]
    public async Task GetAsync_ReturnsSuccessWithNullResult_WhenAttributeNotSet()
    {
        var (client, _) = CreateClient(_ => Json(HttpStatusCode.OK, Rows()));

        var result = await client.GetAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeTrue();
        result.Result.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_ReturnsConflict_WhenSeveralRowsExist()
    {
        var (client, _) = CreateClient(_ => Json(HttpStatusCode.OK, Rows(Row(5), Row(6))));

        var result = await client.GetAsync(_product, TopProductTypeId);

        result.StatusCode.Should().Be(HttpStatusCode.Conflict);
        result.ErrorMessage.Should().Contain("5, 6");
    }

    [Fact]
    public async Task GetAsync_ReturnsFailureWithFlexiMessage_WhenServerRejects()
    {
        var (client, _) = CreateClient(_ => Json(HttpStatusCode.Unauthorized, "{\"winstrom\":{\"success\":\"false\",\"message\":\"Je potřebná autorizace.\"}}"));

        var result = await client.GetAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.ErrorMessage.Should().Be("Je potřebná autorizace.");
    }

    [Fact]
    public async Task GetAsync_ReturnsServiceUnavailable_WhenTransportFails()
    {
        var (client, _) = CreateClient(_ => throw new HttpRequestException("connection refused"));

        var result = await client.GetAsync(_product, TopProductTypeId);

        result.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        result.ErrorMessage.Should().Contain("connection refused");
    }

    [Fact]
    public async Task GetAsync_ReturnsBadGateway_WhenResponseIsNotJson()
    {
        var (client, _) = CreateClient(_ => Json(HttpStatusCode.OK, "<html>oops</html>"));

        var result = await client.GetAsync(_product, TopProductTypeId);

        result.StatusCode.Should().Be(HttpStatusCode.BadGateway);
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task SetAsync_CreatesRow_WhenProductHasNone()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.IsSuccess.Should().BeTrue();
        var post = handler.Requests.Single(r => r.Method == HttpMethod.Post);
        post.Url.Should().Be("https://test.flexibee.eu/c/test/atribut");
        post.Body.Should().Be("{\"winstrom\":{\"atribut\":{\"cenik\":\"code:ABC001\",\"typAtributu\":90,\"valBoolean\":true}}}");
    }

    [Fact]
    public async Task SetAsync_UpdatesExistingRow_InsteadOfCreatingDuplicate()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows(Row(5, "false"))));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.IsSuccess.Should().BeTrue();
        handler.Requests.Single(r => r.Method == HttpMethod.Post).Body
            .Should().Be("{\"winstrom\":{\"atribut\":{\"id\":5,\"valBoolean\":true}}}");
    }

    [Fact]
    public async Task SetAsync_CanStoreExplicitFalse()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows(Row(5))));

        await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(false));

        handler.Requests.Single(r => r.Method == HttpMethod.Post).Body.Should().Contain("\"valBoolean\":false");
    }

    [Fact]
    public async Task SetAsync_IsIdempotent_SecondCallUpdatesTheRowTheFirstCreated()
    {
        var rows = Rows();
        var (client, handler) = CreateClient(r =>
        {
            if (r.Method == HttpMethod.Post)
            {
                rows = Rows(Row(42));
                return Json(HttpStatusCode.OK, OkWrite);
            }
            return r.Url.Contains("/typ-atributu/") ? Json(HttpStatusCode.OK, TypeRow(BooleanTypeKey)) : Json(HttpStatusCode.OK, rows);
        });

        await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));
        await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        var posts = handler.Requests.Where(r => r.Method == HttpMethod.Post).ToList();
        posts[0].Body.Should().Contain("\"cenik\"");
        posts[1].Body.Should().Contain("\"id\":42").And.NotContain("\"cenik\"");
    }

    [Fact]
    public async Task SetAsync_ReturnsBadRequestWithoutWriting_WhenValueKindDoesNotMatchDefinition()
    {
        var (client, handler) = CreateClient(Route("typAtribut.retezec", Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.ErrorMessage.Should().Contain("typAtribut.retezec").And.Contain("TOPPRODUKT");
        handler.Requests.Should().OnlyContain(r => r.Method == HttpMethod.Get);
    }

    [Fact]
    public async Task SetAsync_ReturnsNotFoundWithoutWriting_WhenAttributeTypeDoesNotExist()
    {
        var (client, handler) = CreateClient(r => r.Url.Contains("/typ-atributu/")
            ? Json(HttpStatusCode.OK, "{\"winstrom\":{\"typ-atributu\":[]}}")
            : Json(HttpStatusCode.OK, Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        handler.Requests.Should().OnlyContain(r => r.Method == HttpMethod.Get);
    }

    [Fact]
    public async Task SetAsync_PropagatesTypeLookupFailure_WithoutWriting()
    {
        var (client, handler) = CreateClient(r => r.Url.Contains("/typ-atributu/")
            ? Json(HttpStatusCode.NotFound, "{\"winstrom\":{\"success\":\"false\",\"message\":\"Záznam nebyl nalezen\"}}")
            : Json(HttpStatusCode.OK, Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.ErrorMessage.Should().Be("Záznam nebyl nalezen");
        handler.Requests.Should().OnlyContain(r => r.Method == HttpMethod.Get);
    }

    [Fact]
    public async Task SetAsync_ReturnsConflictWithoutWriting_WhenSeveralRowsExist()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows(Row(5), Row(6))));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.StatusCode.Should().Be(HttpStatusCode.Conflict);
        handler.Requests.Should().OnlyContain(r => r.Method == HttpMethod.Get);
    }

    [Fact]
    public async Task SetAsync_ReturnsFailure_WhenWriteIsRejected()
    {
        var (client, _) = CreateClient(r => r.Method == HttpMethod.Post
            ? Json(HttpStatusCode.BadRequest, "{\"winstrom\":{\"success\":\"false\",\"message\":\"Chyba\"}}")
            : r.Url.Contains("/typ-atributu/") ? Json(HttpStatusCode.OK, TypeRow(BooleanTypeKey)) : Json(HttpStatusCode.OK, Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.ErrorMessage.Should().Be("Chyba");
    }

    [Fact]
    public async Task SetAsync_ReturnsFailure_WhenWriteFailsAtTransportLevel()
    {
        var (client, _) = CreateClient(r => r.Method == HttpMethod.Post
            ? throw new HttpRequestException("connection reset")
            : r.Url.Contains("/typ-atributu/") ? Json(HttpStatusCode.OK, TypeRow(BooleanTypeKey)) : Json(HttpStatusCode.OK, Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        result.ErrorMessage.Should().Contain("connection reset");
    }

    [Fact]
    public async Task SetAsync_ReturnsFailure_WhenBodyReportsFailureDespiteOkStatus()
    {
        var (client, _) = CreateClient(r => r.Method == HttpMethod.Post
            ? Json(HttpStatusCode.OK, "{\"winstrom\":{\"success\":\"false\",\"message\":\"Odmítnuto\"}}")
            : r.Url.Contains("/typ-atributu/") ? Json(HttpStatusCode.OK, TypeRow(BooleanTypeKey)) : Json(HttpStatusCode.OK, Rows()));

        var result = await client.SetAsync(_product, TopProductTypeId, ProductAttributeValue.FromBoolean(true));

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Odmítnuto");
    }

    [Fact]
    public async Task ClearAsync_ReturnsFailure_WhenDeleteIsRejected()
    {
        var (client, _) = CreateClient(r => r.Method == HttpMethod.Post
            ? Json(HttpStatusCode.BadRequest, "{\"winstrom\":{\"success\":\"false\",\"message\":\"Nelze smazat\"}}")
            : Json(HttpStatusCode.OK, Rows(Row(5))));

        var result = await client.ClearAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Nelze smazat");
    }

    [Fact]
    public async Task GetAsync_EscapesSpecialCharactersInProductCode()
    {
        var (client, handler) = CreateClient(_ => Json(HttpStatusCode.OK, Rows()));

        await client.GetAsync(ProductReference.FromCode("A/B (1)"), TopProductTypeId);

        handler.Requests.Single().Url.Should().Contain("(cenik='code%3AA%2FB%20%281%29'%20and%20typAtributu=90)");
    }

    [Fact]
    public async Task SetAsync_StoresStringIntegerAndNumericThroughMatchingDefinition()
    {
        var cases = new (ProductAttributeValue Value, string TypeKey, string Fragment)[]
        {
            (ProductAttributeValue.FromString("L1"), "typAtribut.retezec", "\"valString\":\"L1\""),
            (ProductAttributeValue.FromInteger(30), "typAtribut.integer", "\"valInteger\":30"),
            (ProductAttributeValue.FromNumeric(1.25m), "typAtribut.numeric", "\"valNumeric\":1.25"),
        };

        foreach (var c in cases)
        {
            var (client, handler) = CreateClient(Route(c.TypeKey, Rows()));

            var result = await client.SetAsync(_product, TopProductTypeId, c.Value);

            result.IsSuccess.Should().BeTrue();
            handler.Requests.Single(r => r.Method == HttpMethod.Post).Body.Should().Contain(c.Fragment);
        }
    }

    [Fact]
    public async Task ClearAsync_DeletesExistingRow()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows(Row(5))));

        var result = await client.ClearAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeTrue();
        handler.Requests.Single(r => r.Method == HttpMethod.Post).Body
            .Should().Be("{\"winstrom\":{\"atribut\":[{\"id\":5,\"@action\":\"delete\"}]}}");
    }

    [Fact]
    public async Task ClearAsync_DeletesAllDuplicateRows()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows(Row(5), Row(6))));

        await client.ClearAsync(_product, TopProductTypeId);

        handler.Requests.Single(r => r.Method == HttpMethod.Post).Body
            .Should().Contain("\"id\":5").And.Contain("\"id\":6");
    }

    [Fact]
    public async Task ClearAsync_SucceedsWithoutWriting_WhenAttributeNotSet()
    {
        var (client, handler) = CreateClient(Route(BooleanTypeKey, Rows()));

        var result = await client.ClearAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeTrue();
        handler.Requests.Should().OnlyContain(r => r.Method == HttpMethod.Get);
    }

    [Fact]
    public async Task ClearAsync_ReturnsFailure_WhenLookupFails()
    {
        var (client, handler) = CreateClient(_ => Json(HttpStatusCode.InternalServerError, "boom"));

        var result = await client.ClearAsync(_product, TopProductTypeId);

        result.IsSuccess.Should().BeFalse();
        handler.Requests.Should().OnlyContain(r => r.Method == HttpMethod.Get);
    }

    [Fact]
    public async Task Methods_ThrowArgumentException_ForInvalidArguments()
    {
        var (client, _) = CreateClient(_ => Json(HttpStatusCode.OK, Rows()));

        await FluentActions.Awaiting(() => client.GetAsync(null!, TopProductTypeId)).Should().ThrowAsync<ArgumentNullException>();
        await FluentActions.Awaiting(() => client.ClearAsync(_product, -1)).Should().ThrowAsync<ArgumentOutOfRangeException>();
        await FluentActions.Awaiting(() => client.SetAsync(_product, TopProductTypeId, null!)).Should().ThrowAsync<ArgumentNullException>();
    }
}
