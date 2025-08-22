using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Accounting.Departments;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Departments;

public class DepartmentClient : ResourceClient, IDepartmentClient
{
    public DepartmentClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<DepartmentClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.Department;
    protected override string? RequestIdentifier => null;

    public async Task<IReadOnlyList<DepartmentFlexiDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder().Build();
        var result = await GetAsync<DepartmentFlexiDto>(query, cancellationToken: cancellationToken);
        return result.ToList().AsReadOnly();
    }
}