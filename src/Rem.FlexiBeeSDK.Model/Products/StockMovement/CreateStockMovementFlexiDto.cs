using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementFlexiDto
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; set; }

    [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
    public string Code { get; set; }

    [JsonProperty("datVyst")]
    public DateTime IssueDate { get; set; }

    [JsonProperty("datUcto", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? AccountingDate { get; set; }

    [JsonProperty("typPohybuK")]
    public string MovementTypeRaw => $"typPohybu.{(Direction == StockMovementDirection.In ? "prijem" : "vydej")}";

    [JsonIgnore]
    public StockMovementDirection Direction { get; set; }

    [JsonProperty("typPohybuSkladK")]
    public string StockMovementTypeRaw { get; set; }

    [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }

    [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
    public bool? WithoutItems { get; set; }

    [JsonProperty("nazFirmy", NullValueHandling = NullValueHandling.Ignore)]
    public string CompanyName { get; set; }

    [JsonProperty("typDokl")]
    public string DocumentTypeCode { get; set; }

    [JsonProperty("sklad")]
    public string WarehouseCode { get; set; }

    [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
    public string CurrencyCode { get; set; }

    [JsonProperty("stredisko", NullValueHandling = NullValueHandling.Ignore)]
    public string DepartmentCode { get; set; }

    [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
    public string VariableSymbol { get; set; }

    [JsonProperty("cisObj", NullValueHandling = NullValueHandling.Ignore)]
    public string OrderNumber { get; set; }

    [JsonProperty("cisDodak", NullValueHandling = NullValueHandling.Ignore)]
    public string DeliveryNote { get; set; }

    [JsonProperty("skladovePolozky", NullValueHandling = NullValueHandling.Ignore)]
    public List<CreateStockMovementItemFlexiDto> Items { get; set; }
}
