using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Response;

public class Result
{
    [JsonProperty("id")]
    public string? Id { get; set; }
    [JsonProperty("ref")]
    public string? Reference { get; set; }
    [JsonProperty("request-id")]
    public string? Requestid { get; set; }
    [JsonProperty("errors")]
    public List<Error>? Errors { get; set; }
}