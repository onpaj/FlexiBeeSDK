using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public class StockItemsMovementUpsertRequestFlexiDto
{
    [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
    public bool? WithoutItems { get; set; }

    [JsonProperty("createdBy", NullValueHandling = NullValueHandling.Ignore)]
    public string CreatedBy { get; set; }

    [JsonProperty("datUcto", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime AccountingDate { get; set; }

    [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime IssueDate { get; set; }

    [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }

    [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
    public string Store { get; set; }

    [JsonProperty("skladovePolozky", NullValueHandling = NullValueHandling.Ignore)]
    public List<StockItemsMovementUpsertRequestItemFlexiDto> StockItems { get; set; }

    [JsonProperty("skladovePolozky@removeAll", NullValueHandling = NullValueHandling.Ignore)]
    public bool? StockItemsRemoveAll { get; set; }

    [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
    public string DocumentTypeRaw => $"code:{DocumentTypeCode}";
    public string DocumentTypeCode { get; set; }

    [JsonProperty("typPohybuK", NullValueHandling = NullValueHandling.Ignore)]
    public string MovementTypeString => $"typPohybu.{(StockMovementDirection == StockMovementDirection.In ? "prijem" : "vydej")}";

    public StockMovementDirection StockMovementDirection { get; set; }
    
    [JsonProperty("typPohybuSkladK", NullValueHandling = NullValueHandling.Ignore)]
    public string StockMovementTypeString => $"typPohybuSklad.{(StockMovementDirection == StockMovementDirection.In ? "prijemHoly" : "vydejHoly")}";

    [JsonProperty("typUcOp", NullValueHandling = NullValueHandling.Ignore)]
    public string AccountingOperationType { get; set; } = "42";

    [JsonProperty("uzivatel", NullValueHandling = NullValueHandling.Ignore)]
    public string User { get; set; }
}