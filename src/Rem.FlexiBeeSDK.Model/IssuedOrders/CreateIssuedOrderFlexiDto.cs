using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class CreateIssuedOrderFlexiDto
{
    [JsonProperty("bezPolozek")] public bool NoItems => false;

    [JsonProperty("cisDosle")]
    public string OrderInternalNumber { get; set; }

    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; }

    [JsonProperty("datSazbyDph")]
    public DateTime DateVat { get; set; }

    [JsonProperty("datVyst")]
    public DateTime DateCreated { get; set; }

    [JsonProperty("polozkyObchDokladu")]
    public List<IssuedOrderItemFlexiDto> Items { get; set; }

    [JsonProperty("polozkyObchDokladu@removeAll")]
    public bool RemoveItemsBeforeCreatingThem => true;

    [JsonProperty("popis")]
    public string Description { get; set; }

    [JsonProperty("poznam")]
    public string Note { get; set; }

    [JsonProperty("stredisko")] public string DepartmentFormatted => $"code:{DepartmentCode}";
    public string DepartmentCode { get; set; }

    [JsonProperty("typDokl")] public string DocumentTypeFormatted => $"code:{DocumentType}";
    public string DocumentType { get; set; }

    [JsonProperty("typDoklNabFak")]
    public string WarehouseDocumentTypeFormated => $"code:{WarehouseDocumentType}";
    
    [JsonIgnore]
    public string WarehouseDocumentType { get; set; }

    [JsonProperty("uzivatel")]
    public string User { get; set; }
}