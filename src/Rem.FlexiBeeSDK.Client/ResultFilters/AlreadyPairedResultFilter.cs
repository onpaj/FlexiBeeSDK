using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.ResultFilters;

public class AlreadyPairedResultFilter : ParseErrorResultFilter
{
    public AlreadyPairedResultFilter(string matchPhrase, ErrorType errorType) : base("protože je spárován", ErrorType.InvoicePaired)
    {
    }
}