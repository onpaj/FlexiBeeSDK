using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public class UpdateBoMIngredientAmountRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("mnozstvi")]
    public double Amount { get; set; }
}
