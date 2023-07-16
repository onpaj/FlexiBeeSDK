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
        
    public Task ApplyAsync(FlexiResult resultData)
    {
        foreach (var error in resultData.Results?.SelectMany(s => s.Errors) ?? new List<Error>())
        {
            if (error?.Message?.Contains(_matchPhrase) ?? false)
                error.ErrorType = _errorType;
        }

        return Task.CompletedTask;
    }
}