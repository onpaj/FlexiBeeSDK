using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public sealed class UpdateBoMItemRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("poradi", NullValueHandling = NullValueHandling.Ignore)]
    public int? Order { get; set; }

    [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    [JsonProperty("nazevA", NullValueHandling = NullValueHandling.Ignore)]
    public string? NameA { get; set; }

    [JsonProperty("nazevB", NullValueHandling = NullValueHandling.Ignore)]
    public string? NameB { get; set; }

    [JsonProperty("nazevC", NullValueHandling = NullValueHandling.Ignore)]
    public string? NameC { get; set; }
}
