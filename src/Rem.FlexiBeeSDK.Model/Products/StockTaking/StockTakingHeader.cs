using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockTaking;

public class StockTakingHeader
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [JsonProperty("popisInventury")]
    public string Description { get; set; }

    [JsonProperty("typInventury")]
    public string Type { get; set; }

    [JsonProperty("datZahaj")]
    public DateTime? DateStarted { get; set; }

    [JsonProperty("datKonec")]
    public DateTime? DateFinished { get; set; }

    [JsonProperty("vedouci")]
    public string Owner { get; set; }

    [JsonProperty("osoby")]
    public string Executor { get; set; }

    [JsonProperty("poznam")]
    public string Notes { get; set; }

    [JsonProperty("popis")]
    public string Description2 { get; set; }

    [JsonProperty("stavK")]
    public string State { get; set; }

    [JsonProperty("sklad")]
    public string Warehouse { get; set; }
}