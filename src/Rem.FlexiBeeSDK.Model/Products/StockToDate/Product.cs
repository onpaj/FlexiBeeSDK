using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockToDate;

public class Product
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("baleniNazev1")]
    public string MoqName { get; set; }
    
    [JsonProperty("baleniMj1")]
    public string MoqAmount { get; set; }
    
    [JsonProperty("evidSarze")]
    public bool HasLots { get; set; }
    
    [JsonProperty("evidExpir")]
    public bool HasExpiration { get; set; }
    
    [JsonProperty("objem")]
    public double Volume { get; set; }

    [JsonProperty("hmotMj")]
    public double Weight { get; set; }
}