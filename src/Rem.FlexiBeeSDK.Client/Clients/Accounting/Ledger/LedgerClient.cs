using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public class LedgerClient : ResourceClient, ILedgerClient
{
    public LedgerClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler  resultHandler,
        ILogger<LedgerClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.Ledger;
    protected override string? RequestIdentifier => null;

    public async Task<IReadOnlyList<LedgerItemFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, IEnumerable<string>? debitAccountPrefixes = null,IEnumerable<string>? creditAccountPrefixes = null, string? departmentId = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
    {
        var queryDoc = new LedgerRequest(dateFrom, dateTo, debitAccountPrefixes, creditAccountPrefixes, departmentId)
        {
            Limit = limit,
            Start = skip,
        };

        var query = new FlexiQuery();
        var result = await PostAsync<LedgerRequest, LedgerResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.LedgerItems ?? new List<LedgerItemFlexiDto>();
    }
}