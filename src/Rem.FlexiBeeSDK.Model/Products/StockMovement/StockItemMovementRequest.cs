using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockItemMovementRequest
{
    // "(doklSklad.typDokl eq \"56\" and doklSklad.typPohybuK in (\"typPohybu.prijem\") and (doklSklad.datVyst gte \"2025-06-01\" and doklSklad.datVyst lte \"2025-06-30\"))",
    public StockItemMovementRequest(
        DateTime dateFrom, 
        DateTime dateTo, 
        StockMovementDirection direction, 
        string? storeCode = null,
        int? documentTypeId = null,
        string? documentCode = null
    )
    {
        Filter =
            $"((doklSklad.datVyst gte \"{dateFrom:yyyy-MM-dd}\" and doklSklad.datVyst lte \"{dateTo:yyyy-MM-dd}\") {GetDirectionFilterString(direction)} {GetDocumentTypeFilterString(documentTypeId)} {GetDocumentNumberFilterString(documentCode)} {GetStoreCodeFilterString(storeCode)})";
    }
    
    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:doklSklad(id,typDokl,kod,typPohybuK),datVyst,nazev,mnozMj,cenaMj,sumCelkem,sklad(id,nazev,kod),cenik(id,kod),expirace,storno,stornoPol,sarze,id";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/skladovy-pohyb-polozka/doklSklad,/skladovy-pohyb-polozka/doklSklad/skladovy-pohyb/firma,/skladovy-pohyb-polozka/sklad,/skladovy-pohyb-polozka/cenik";

    [JsonProperty("order")] public string Order { get; set; } = "datUcto";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";
    
    [JsonProperty("filter")] public string Filter { get; private set; }

    
    private string GetDocumentTypeFilterString(int? documentTypeId = null)
    {
        if(documentTypeId == null)
            return String.Empty;

        return $"and doklSklad.typDokl eq \"{documentTypeId}\"";
    }
        
    private string GetDocumentNumberFilterString(string? documentNumber = null)
    {
        if(documentNumber == null)
            return String.Empty;

        return $"and doklSklad.kod eq \"{documentNumber}\"";
    }
        
    private string GetStoreCodeFilterString(string? storeCode = null)
    {
        if(storeCode == null)
            return String.Empty;

        return $"and sklad.kod eq \"{storeCode}\"";
    }
        
    private string GetDirectionFilterString(StockMovementDirection direction)
    {
        if(direction == StockMovementDirection.Any)
            return String.Empty;

        return $"and doklSklad.typPohybuK in (\"typPohybu.{(direction == StockMovementDirection.In ? "prijem" : "vydej")}\")";
    }
}