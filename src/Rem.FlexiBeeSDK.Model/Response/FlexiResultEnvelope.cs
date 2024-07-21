using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Response;

public class FlexiResultEnvelope
{
    [JsonProperty("winstrom")]
    public OperationResultDetail Data { get; set; }
}