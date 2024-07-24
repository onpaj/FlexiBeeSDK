using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public class BoMRequest
{
    public const string RecalculatePurchasePriceActionName = "prepocti-nakupni-cenu";

    [JsonProperty("id")]
    public int BomId { get; set; }

    [JsonProperty("@action")]
    public string Action { get; set; } = RecalculatePurchasePriceActionName;
}