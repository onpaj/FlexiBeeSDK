using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.Ledger;

public class LedgerRequest
{
    public LedgerRequest(DateTime dateFrom, DateTime dateTo, string? debitAccountPrefix = null, string? creditAccountPrefix = null, string? departmentId = null)
    {
        Filter =
            $"((datUcto gte \"{dateFrom:yyyy-MM-dd}\" and datUcto lte \"{dateTo:yyyy-MM-dd}\") {GetAccountFilterString("mdUcet", debitAccountPrefix)} {GetAccountFilterString("dalUcet", creditAccountPrefix)}  {GetDepartmentFilterString(departmentId)})";
    }
    
    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:parSymbol,datVyst,datUcto,doklad,nazFirmy,stredisko(nazev,kod,id),popis,sumTuz,sumMen,mena(kod),kurz,mdUcet(kod,nazev,id),dalUcet(kod,nazev,id),zuctovano,idUcetniDenik,idDokl";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/ucetni-denik/stredisko,/ucetni-denik/mena,/ucetni-denik/mdUcet,/ucetni-denik/dalUcet";

    [JsonProperty("order")] public string Order { get; set; } = "datUcto";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";
    
    [JsonProperty("filter")] public string Filter { get; private set; }

    private string GetAccountFilterString(string fieldName, string? accountPrefix = null)
    {
        if(accountPrefix == null)
            return String.Empty;

        return $"and {fieldName}.kod begins \"{accountPrefix}\"";
    }
    
    private string GetDepartmentFilterString(string? departmentId = null)
    {
        if(departmentId == null)
            return String.Empty;

        return $"and stredisko.kod = \"{departmentId}\"";
    }
}