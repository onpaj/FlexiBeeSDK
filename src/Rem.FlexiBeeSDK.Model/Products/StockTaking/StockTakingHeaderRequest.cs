using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockTaking;

public class StockTakingHeaderRequest
{
    [JsonProperty("datKonec")] 
    public DateTime DatKonec => Date;

    [JsonProperty("datZahaj")]
    public DateTime Date { get; set; }

    [JsonProperty("osoby")]
    public string Executer { get; set; }

    [JsonProperty("popis")]
    public string Name { get; set; }

    [JsonProperty("popisInventury")]
    public string Description { get; set; }

    [JsonProperty("sklad")]
    public int WarehouseId { get; set; }

    [JsonProperty("typInventury")]
    public string Type { get; set; }

    [JsonProperty("vedouci")]
    public string Owner { get; set; }
}