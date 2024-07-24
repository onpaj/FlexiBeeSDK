using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public class RecalculatePriceRequest
{

    [JsonProperty("kusovnik")]
    public BoMRequest BoM { get; set; }
    
    
    
    public RecalculatePriceRequest(int bomId)
    {
        BoM = new BoMRequest()
        {
            BomId = bomId
        };
    }
}