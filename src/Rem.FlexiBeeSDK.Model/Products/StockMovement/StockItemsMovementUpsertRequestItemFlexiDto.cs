using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public class StockItemsMovementUpsertRequestItemFlexiDto
{
    [JsonProperty("autogen", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Autogen { get; set; }

    [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
    public string Id => $"code:{ProductCode}";

    [JsonProperty("cenaMj", NullValueHandling = NullValueHandling.Ignore)]
    public double? UnitPrice { get; set; }

    [JsonProperty("expirace", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? Expiration { get; set; }

    public string ProductCode { get; set; }

    [JsonProperty("mnozMj", NullValueHandling = NullValueHandling.Ignore)]
    public int? Amount { get; set; }

    [JsonProperty("mnozMjPrijem", NullValueHandling = NullValueHandling.Ignore)]
    public int? AmountReceived { get; set; }

    [JsonProperty("mnozMjVydej", NullValueHandling = NullValueHandling.Ignore)]
    public int? AmountIssued { get; set; }

    [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
    public string ProductName { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }

    [JsonProperty("sarze", NullValueHandling = NullValueHandling.Ignore)]
    public string? LotNumber { get; set; }

    [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
    public string WarehouseCode { get; set; }
}