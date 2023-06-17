using System.Collections.Generic;
using Newtonsoft.Json;

public class Result
{
    [JsonProperty("request-id")]
    public string Requestid { get; set; }
    [JsonProperty("errors")]
    public List<Error> Errors { get; set; }
}