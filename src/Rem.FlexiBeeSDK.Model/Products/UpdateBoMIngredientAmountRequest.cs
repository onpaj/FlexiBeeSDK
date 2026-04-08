using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public class UpdateBoMIngredientAmountRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("mnoz")]
    public double Amount { get; set; }
}
