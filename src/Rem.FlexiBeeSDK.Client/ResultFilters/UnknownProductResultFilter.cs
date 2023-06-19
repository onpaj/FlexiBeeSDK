using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public class UnknownProductResultFilter : ParseErrorResultFilter
{
    public UnknownProductResultFilter(string matchPhrase, ErrorType errorType) :
        base("musí identifikovat objekt", ErrorType.ProductNotFound)
    {
    }
}