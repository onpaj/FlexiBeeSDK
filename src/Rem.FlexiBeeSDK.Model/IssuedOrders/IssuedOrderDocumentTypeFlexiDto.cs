using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class IssuedOrderDocumentTypeFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }
}