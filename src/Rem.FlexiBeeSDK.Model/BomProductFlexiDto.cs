using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model;

public class BomProductFlexiDto
{
    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("nakupCena")]
    public double PurchasePrice { get; set; }
    
    [JsonProperty("skupZboz@internalId")]
    public int ProductTypeId { get; set; }
    
    [JsonProperty("evidSarze")]
    public bool HasLots { get; set; }
    
    [JsonProperty("evidExpir")]
    public bool HasExpiration { get; set; }
}