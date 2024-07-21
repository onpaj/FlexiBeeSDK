using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public interface IResultHandler
{
    Task ApplyFiltersAsync<TResult>(object resultData);
}