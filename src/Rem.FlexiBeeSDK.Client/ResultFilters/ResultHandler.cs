using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public class ResultHandler : IResultHandler
{
    private readonly IEnumerable<IResultFilter> _filters;

    public ResultHandler(IEnumerable<IResultFilter> filters)
    {
        _filters = filters;
    }

    public async Task ApplyFiltersAsync<TResult>(object resultData)
    {
        foreach (var s in _filters.Where(w => w.CanHandle<TResult>(resultData)))
        {
            await s.ApplyAsync(resultData);
        }
    }
}