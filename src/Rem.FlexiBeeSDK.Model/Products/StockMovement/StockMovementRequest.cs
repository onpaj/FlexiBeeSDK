using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementRequest
{
    public StockMovementRequest(int id)
    {
        Filter = $"(id eq {id})";
    }

    public StockMovementRequest(string code)
    {
        Filter = $"(kod eq \"{code}\")";
    }

    public StockMovementRequest(
        DateTime dateFrom,
        DateTime dateTo,
        StockMovementDirection? direction = null,
        string? warehouseCode = null,
        int? documentTypeId = null)
    {
        var directionFilter = GetDirectionFilter(direction);
        var warehouseFilter = GetWarehouseFilter(warehouseCode);
        var docTypeFilter = GetDocumentTypeFilter(documentTypeId);

        Filter = $"((datVyst gte \"{dateFrom:yyyy-MM-dd}\" and datVyst lte \"{dateTo:yyyy-MM-dd}\"){directionFilter}{warehouseFilter}{docTypeFilter})";
    }

    [JsonProperty("add-row-count")]
    public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:id,kod,lastUpdate,datVyst,datUcto,typPohybuK,typPohybuSkladK,popis,poznam,sumCelkem,sumCelkemMen,storno,zuctovano,bezPolozek,nazFirmy,typDokl(id,kod,nazev),sklad(id,kod,nazev),mena(id,kod,nazev),stredisko(id,kod,nazev),varSym,cisObj,cisDodak,skladovePolozky";

    [JsonProperty("limit")]
    public int Limit { get; set; } = 0;

    [JsonProperty("start")]
    public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/skladovy-pohyb/typDokl,/skladovy-pohyb/sklad,/skladovy-pohyb/mena,/skladovy-pohyb/stredisko,/skladovy-pohyb/skladovePolozky";

    [JsonProperty("order")]
    public string Order { get; set; } = "datVyst@D";

    [JsonProperty("use-internal-id")]
    public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")]
    public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")]
    public string Version { get; set; } = "1.0";

    [JsonProperty("filter")]
    public string Filter { get; private set; }

    private string GetDirectionFilter(StockMovementDirection? direction)
    {
        if (!direction.HasValue || direction == StockMovementDirection.Any)
            return string.Empty;

        var directionValue = direction == StockMovementDirection.In ? "prijem" : "vydej";
        return $" and typPohybuK eq \"typPohybu.{directionValue}\"";
    }

    private string GetWarehouseFilter(string? warehouseCode)
    {
        if (string.IsNullOrEmpty(warehouseCode))
            return string.Empty;

        return $" and sklad.kod eq \"{warehouseCode}\"";
    }

    private string GetDocumentTypeFilter(int? documentTypeId)
    {
        if (!documentTypeId.HasValue)
            return string.Empty;

        return $" and typDokl eq {documentTypeId.Value}";
    }
}
