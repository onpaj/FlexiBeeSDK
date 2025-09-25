using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class IssuedOrderRequest
{
    public IssuedOrderRequest(string documentNumber)
    {
        Filter =
            $"(kod eq \"{documentNumber}\")";
    }
    
    public IssuedOrderRequest(int id)
    {
        Filter =
            $"(id eq \"{id}\")";
    }

    public IssuedOrderRequest(DateTime dateFrom, DateTime dateTo, int? documentTypeId = null)
    {
        Filter =
            $"((datVyst gte \"{dateFrom:yyyy-MM-dd}\" and datVyst lte \"{dateTo:yyyy-MM-dd}\") {GetDocumentTypeFilterString(documentTypeId)} )";
    }


    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:datVyst,kod,typDokl(kod),nazFirmy,stredisko(nazev,kod,id),sumZklCelkemMen,sumZklCelkem,mena(kod),sumCelkemMen,sumCelkem,storno,datTermin,stavDoklObch(nazev,poradi,kod,id),popis,id,zamekK,stavUzivK,polozkyObchDokladu(id,cenik,kod,mnozMj,nazev,sklad)";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/objednavka-vydana/typDokl,/objednavka-vydana/stredisko,/objednavka-vydana/mena,/objednavka-vydana/stavDoklObch,/objednavka-vydana/polozkyObchDokladu";

    [JsonProperty("order")] public string Order { get; set; } = "datVyst@D";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";
    
    [JsonProperty("filter")] public string Filter { get; private set; }

    private string GetDocumentTypeFilterString(int? documentTypeId)
    {
        if(documentTypeId == null)
            return String.Empty;

        return $" and typDokl = \"{documentTypeId}\"";
    }
}