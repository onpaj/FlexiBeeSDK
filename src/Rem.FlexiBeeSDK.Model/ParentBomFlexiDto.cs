using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model;

public class ParentBomFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("mnoz")]
    public double Amount { get; set; }
}