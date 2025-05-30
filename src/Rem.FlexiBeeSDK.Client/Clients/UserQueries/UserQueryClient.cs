using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients.UserQueries;

public abstract class UserQueryClient<T> : ResourceClient, IUserQueryClient<T>
{
    public const string LimitParamName = "limit";
    public UserQueryClient(
        FlexiBeeSettings connection, 
        IHttpClientFactory httpClientFactory,
        IResultHandler  resultHandler,
        ILogger<ReceivedInvoiceClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }
    protected abstract int QueryId { get; }

        
    protected override string ResourceIdentifier => Agenda.UserQuery;
    protected override string ResultIdentifier => "DotazView";
    public async Task<IList<T>> GetAsync(Dictionary<string, string> queryParameters, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder()
            .Raw($"{QueryId}/call")
            .WithParameters(queryParameters)
            .Build();

        var found = await GetAsync<T>(query, cancellationToken: cancellationToken);

        return found;
    }
}