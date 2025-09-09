using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Sets;

public class ProductSetFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("mnozMj")]
    public double Quantity { get; set; }

    [JsonProperty("cenik")]
    public List<ProductSetsProductFlexiDto> ProductList { get; set; }

    public ProductSetsProductFlexiDto Product => ProductList.FirstOrDefault()!;
    
    [JsonProperty("cenikSada@showAs")]
    public string SetName { get; set; }

    [JsonProperty("poznam")]
    public string Note { get; set; }
}