using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockTaking;

public class StockTakingItemResult
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [JsonProperty("mnozMjReal")]
    public double AmountFound { get; set; }

    [JsonProperty("mnozMjKarta")]
    public double AmountErp { get; set; }

    [JsonProperty("sarze")]
    public string? LotCode { get; set; }

    [JsonProperty("expirace")]
    public DateTime? Expiration { get; set; }

    [JsonProperty("skladKarta")]
    public int ProductId { get; set; }

    [JsonProperty("cenik")]
    public string ProductCode { get; set; }
}