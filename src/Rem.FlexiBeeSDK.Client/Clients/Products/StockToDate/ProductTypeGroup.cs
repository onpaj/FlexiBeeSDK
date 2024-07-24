using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public class ProductTypeGroup
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }
}