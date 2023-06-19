using System.Linq;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public abstract class ParseErrorResultFilter : IResultFilter
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
        foreach (var error in resultData.Results.SelectMany(s => s.Errors))
        {
            if (error.Message.Contains(_matchPhrase))
                error.ErrorType = _errorType;
        }

        return Task.CompletedTask;
    }
}