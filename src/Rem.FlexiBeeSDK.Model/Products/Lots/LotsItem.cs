using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Lots;

public class LotsItem
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("sarze")]
    public string Lot { get; set; }
    [JsonProperty("expirace")]
    public DateTime? Expiration { get; set; }
    [JsonProperty("cenik")]
    public string ProductCode { get; set; }
    [JsonProperty("pocet")]
    public decimal Amount { get; set; } 
}