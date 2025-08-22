using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices;

public class CurrencyFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }
}