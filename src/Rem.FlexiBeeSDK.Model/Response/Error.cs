using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Response;

public class Error
{
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("@for")]
    public string? For { get; set; }
    [JsonProperty("path")]
    public string? Path { get; set; }
    [JsonProperty("value")]
    public string? Value { get; set; }
    [JsonProperty("code")]
    public string? Code { get; set; }

    public ErrorType ErrorType { get; set; } = ErrorType.General;
}