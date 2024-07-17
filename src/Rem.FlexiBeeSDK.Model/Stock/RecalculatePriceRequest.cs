using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Stock;

public class RecalculatePriceRequest
{
    public const string RecalculatePurchasePriceActionName = "prepocti-nakupni-cenu";
    
    
    [JsonProperty("id")]
    public int BomId { get; set; }

    [JsonProperty("@action")]
    public string Action { get; set; } = RecalculatePurchasePriceActionName;
    
    public RecalculatePriceRequest(int bomId)
    {
        BomId = bomId;
    }
}