using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Payments;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.CashRegisters;

public class CashRegisterClient : ResourceClient, ICashRegisterClient
{
    public CashRegisterClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<CashRegisterClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.CashRegister;

    public Task<OperationResult<OperationResultDetail>> UnPairPayment(string paymentCode, CancellationToken cancellationToken = default)
    {
        var id = paymentCode.StartsWith("code:") ? paymentCode : $"code:{paymentCode}";
        var request = new List<BankUnpairRequest>
        {
            new() { Id = id }
        };

        return PostAsync(request, cancellationToken: cancellationToken);
    }
}
