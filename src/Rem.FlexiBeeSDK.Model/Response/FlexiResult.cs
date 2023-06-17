using System.Collections.Generic;
using Newtonsoft.Json;

public class FlexiResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }
    [JsonProperty("success")]
    public string Success { get; set; }
    [JsonProperty("stats")]
    public Stats Stats { get; set; }
    [JsonProperty("results")]
    public List<Result> Results { get; set; }
}