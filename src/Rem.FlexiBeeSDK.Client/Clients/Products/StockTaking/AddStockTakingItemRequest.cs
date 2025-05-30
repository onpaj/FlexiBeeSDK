using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;

public class AddStockTakingItemRequest
{
    [JsonProperty("cenik")]
    public string ProductCode { get; set; }

    [JsonProperty("expirace")]
    public DateTime? Expiration { get; set; }

    [JsonProperty("inventura")]
    internal int StockTakingHeaderId { get; set; }

    [JsonProperty("mnozMjReal")]
    public decimal Amount { get; set; }

    [JsonProperty("sarze")]
    public string? Lot { get; set; }

    [JsonProperty("sklad")]
    internal int WarehouseId { get; set; }
}