using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementItemFlexiDto
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; set; }

    [JsonProperty("cenik")]
    public string ProductCode { get; set; }

    [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
    public string WarehouseCode { get; set; }

    [JsonProperty("mnozMj")]
    public decimal Quantity { get; set; }

    [JsonProperty("cenaMj", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? PricePerUnit { get; set; }

    [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }

    [JsonProperty("sarze", NullValueHandling = NullValueHandling.Ignore)]
    public string Batch { get; set; }

    [JsonProperty("expirace", NullValueHandling = NullValueHandling.Ignore)]
    public string Expiration { get; set; }
}
