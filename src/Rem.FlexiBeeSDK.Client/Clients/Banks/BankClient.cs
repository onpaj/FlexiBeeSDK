using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Payments;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Banks;

public class BankClient : ResourceClient, IBankClient
{
    public BankClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<BankClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.Bank;
    public Task<OperationResult<OperationResultDetail>> UnPairPayment(int paymentId, CancellationToken cancellationToken = default)
    {
        var request = new List<BankUnpairRequest>
        {
            new() { Id = paymentId }
        };

        var query = new FlexiQuery
        {
            IncludeQuerySegment = false,
            Parameters = { ["use-internal-id"] = "true", ["no-ext-ids"] = "true" }
        };

        return PostAsync(request, query, cancellationToken: cancellationToken);
    }
}