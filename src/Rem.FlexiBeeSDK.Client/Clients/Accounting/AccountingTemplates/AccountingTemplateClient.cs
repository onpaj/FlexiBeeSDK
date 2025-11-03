using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Accounting;
using Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting;

public class AccountingTemplateClient : ResourceClient, IAccountingTemplateClient
{
    public AccountingTemplateClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<AccountingTemplateClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.AccountingTemplate;
    protected override string? RequestIdentifier => null;

    public async Task<IReadOnlyList<AccountingTemplateFlexiDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var queryDoc = new AccountingTemplateRequest()
        {
        };

        var query = new FlexiQuery();
        var result = await PostAsync<AccountingTemplateRequest, AccountingTemplateResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.AccountingTemplates ?? new List<AccountingTemplateFlexiDto>();
    }
}