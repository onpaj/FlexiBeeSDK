using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class IssuedOrderStockMovementFlexiDto
{
    [JsonProperty("typDokl")]
    public string WarehouseDocumentTypeFormated => $"code:{WarehouseDocumentType}";
    public string WarehouseDocumentType { get; set; }
    
    [JsonProperty("sklad")]
    public string WarehouseCodeFormatted => $"code:{WarehouseCode}";
    public string WarehouseCode { get; set; }

    [JsonProperty("polozkyObchDokladu")]
    public List<FinalizeIssuedOrderItemFlexiDto> Items { get; set; }

    [JsonProperty("generovatPozadavky")]
    public bool FinalizeStockMovement { get; set; } = true;

    [JsonProperty("odpocetZaloh")]
    public bool UseDeposit { get; set; }
}