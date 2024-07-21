using System;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public interface IResultFilter
{
    Task ApplyAsync(object resultData);
    bool CanHandle<TResult>(object resultData);
}

