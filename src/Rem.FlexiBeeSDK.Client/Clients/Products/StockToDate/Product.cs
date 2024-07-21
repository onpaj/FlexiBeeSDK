using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public class Product
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }
}