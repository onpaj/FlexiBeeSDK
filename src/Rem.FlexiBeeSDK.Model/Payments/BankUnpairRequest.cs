using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Payments;

public class BankUnpairRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }
        
    [JsonProperty("odparovani")]
    public List<object> Args { get; set; } = new();
}