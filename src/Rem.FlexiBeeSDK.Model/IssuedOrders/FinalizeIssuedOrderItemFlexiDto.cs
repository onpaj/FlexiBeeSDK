using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class FinalizeIssuedOrderItemFlexiDto
{
    [JsonProperty("id")] public string Id { get; set; }
    
    [JsonProperty("mj")]
    public double Ammount { get; set; }

    [JsonProperty("sarze")]
    public string? LotNumber { get; set; }

    [JsonProperty("expirace")]
    public DateTime? ExpirationDate { get; set; }

}