using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Sets;

public class ProductSetsProductFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }
}