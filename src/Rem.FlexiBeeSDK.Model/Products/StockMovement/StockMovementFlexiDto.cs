using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime? LastUpdate { get; set; }

    [JsonProperty("datVyst")]
    public DateTime IssueDate { get; set; }

    [JsonProperty("datUcto")]
    public DateTime? AccountingDate { get; set; }

    [JsonProperty("typPohybuK")]
    public string MovementTypeRaw { get; set; }

    [JsonProperty("typPohybuSkladK")]
    public string StockMovementTypeRaw { get; set; }

    [JsonProperty("popis")]
    public string Description { get; set; }

    [JsonProperty("poznam")]
    public string Note { get; set; }

    [JsonProperty("sumCelkem")]
    public decimal TotalAmount { get; set; }

    [JsonProperty("sumCelkemMen")]
    public decimal TotalAmountCurrency { get; set; }

    [JsonProperty("storno")]
    public bool Cancelled { get; set; }

    [JsonProperty("zuctovano")]
    public bool Posted { get; set; }

    [JsonProperty("bezPolozek")]
    public bool WithoutItems { get; set; }

    [JsonProperty("nazFirmy")]
    public string CompanyName { get; set; }

    [JsonProperty("typDokl")]
    public List<StockMovementDocumentTypeFlexiDto> DocumentTypeList { get; set; }

    [JsonProperty("sklad")]
    public List<StockMovementWarehouseFlexiDto> WarehouseList { get; set; }

    [JsonProperty("mena")]
    public List<StockMovementCurrencyFlexiDto> CurrencyList { get; set; }

    [JsonProperty("stredisko")]
    public List<StockMovementDepartmentFlexiDto> DepartmentList { get; set; }

    [JsonProperty("skladovePolozky")]
    public List<StockItemMovementFlexiDto> Items { get; set; }

    [JsonProperty("varSym")]
    public string VariableSymbol { get; set; }

    [JsonProperty("cisObj")]
    public string OrderNumber { get; set; }

    [JsonProperty("cisDodak")]
    public string DeliveryNote { get; set; }
}
