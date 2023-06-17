using Newtonsoft.Json;

public class FlexiResultEnvelope
{
    [JsonProperty("winstrom")]
    public FlexiResult Data { get; set; }
}