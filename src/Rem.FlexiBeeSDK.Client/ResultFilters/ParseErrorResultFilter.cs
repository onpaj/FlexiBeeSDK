using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public class ParseErrorResultFilter : IResultFilter
{
    private readonly string _matchPhrase;
    private readonly ErrorType _errorType;

    public ParseErrorResultFilter(string matchPhrase, ErrorType errorType)
    {
        _matchPhrase = matchPhrase;
        _errorType = errorType;
    }
        
    public Task ApplyAsync(OperationResultDetail resultData)
    {
        foreach (var error in resultData.Results?.Where(w => w.Errors != null && w.Errors.Any()).SelectMany(s => s?.Errors) ?? new List<Error>())
        {
            if (error?.Message?.Contains(_matchPhrase) ?? false)
                error.ErrorType = _errorType;
        }

        return Task.CompletedTask;
    }

    public Task ApplyAsync(object resultData) => ApplyAsync((OperationResultDetail)resultData);

    public bool CanHandle<TResult>(object resultData) => typeof(TResult) == typeof(OperationResultDetail);
}