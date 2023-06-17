using Newtonsoft.Json;

public class Stats
{
    [JsonProperty("created")]
    public string Created { get; set; }
    [JsonProperty("updated")]
    public string Updated { get; set; }
    [JsonProperty("deleted")]
    public string Deleted { get; set; }
    [JsonProperty("skipped")]
    public string Skipped { get; set; }
    [JsonProperty("failed")]
    public string Failed { get; set; }
}