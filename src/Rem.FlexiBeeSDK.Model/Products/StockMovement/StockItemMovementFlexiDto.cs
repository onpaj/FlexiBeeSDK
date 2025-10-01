using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockItemMovementFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("doklSklad")]
    public List<StockItemDocumentFlexiDto> DocumentList { get; set; }

    public StockItemDocumentFlexiDto Document => DocumentList.First();
    
    [JsonProperty("datVyst")]
    public DateTime Date { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("mnozMj")]
    public double Amount { get; set; }

    [JsonProperty("cenaMj")]
    public double PricePerUnit { get; set; }

    [JsonProperty("sumCelkem")]
    public double TotalSum { get; set; }

    [JsonProperty("sklad@evidencePath")]
    public string StoreEvidencePath { get; set; }

    [JsonProperty("sklad@internalId")]
    public int StoreInternalId { get; set; }

    [JsonProperty("sklad@ref")]
    public string StoreRef { get; set; }

    [JsonProperty("sklad@showAs")]
    public string StoreShowAs { get; set; }

    [JsonProperty("sklad")]
    public List<StockItemStoreFlexiDto> StoreList { get; set; }

    public string StoreCode => StoreList.First().Code;

    [JsonProperty("cenik@evidencePath")]
    public string PriceListEvidencePath { get; set; }

    [JsonProperty("cenik@internalId")]
    public int PriceListInternalId { get; set; }

    [JsonProperty("cenik@ref")]
    public string PriceListRef { get; set; }

    [JsonProperty("cenik@showAs")]
    public string PriceListShowAs { get; set; }

    [JsonProperty("cenik")]
    public List<StockItemProductFlexiDto> Items { get; set; }

    public string ProductCode => Items.First().Code;

    [JsonProperty("expirace")]
    public string Expiration { get; set; }

    [JsonProperty("storno")]
    public bool Cancelled { get; set; }

    [JsonProperty("stornoPol")]
    public bool ItemCancelled { get; set; }

    [JsonProperty("sarze")]
    public string Batch { get; set; }
}