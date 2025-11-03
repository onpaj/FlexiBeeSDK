using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices;

public class ReceivedInvoiceRequest
{
    public ReceivedInvoiceRequest(DateTime dateFrom, DateTime dateTo, string? label = null, string? accountingTemplate = null, string? documentNumber = null, string? companyId = null)
    {
        Filter =
            $"((datVyst gte \"{dateFrom:yyyy-MM-dd}\" and datVyst lte \"{dateTo:yyyy-MM-dd}\") {GetLabelFilterString(label)} {GetAccountingTemplateFilterString(accountingTemplate)} {GetDocumentNumberFilterString(documentNumber)} {GetCompanyIdFilterString(companyId)})";
    }
    
    
    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:datVyst,kod,typDokl(kod,typDoklK),nazFirmy,cisDosle,varSym,stredisko(id,kod),datSplat,sumZklCelkemMen,sumZklCelkem,mena(kod,id),sumCelkemMen,sumCelkem,juhSum,stavUhrK,juhSumMen,storno,popis,buc,stitky,smerKod(kod),iban,bic,zuctovano,datUcto,typUcOp(nazev,kod,id),id,podpisPrik,zamekK,stavOdpocetK,stavUzivK,bezPolozek,firma,ic,polozkyDokladu(id,nazev,kod,mnozMj,sumZkl,sumDph,sumCelkem,cenaMj,poznam)";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/faktura-prijata/typDokl,/faktura-prijata/stredisko,/faktura-prijata/mena,/faktura-prijata/smerKod,/faktura-prijata/typUcOp,/faktura-prijata/polozkyDokladu";

    [JsonProperty("order")] public string Order { get; set; } = "datVyst";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";
    
    [JsonProperty("filter")] public string Filter { get; private set; }

        
    private string GetLabelFilterString(string? label = null)
    {
        if(label == null)
            return String.Empty;

        return $"and stitky eq \"code:{label}\"";
    }
    
    private string GetAccountingTemplateFilterString(string? accountingTemplate = null)
    {
        if(accountingTemplate == null)
            return String.Empty;

        return $"and typUcOp.kod eq \"{accountingTemplate}\"";
    }
    
    private string GetDocumentNumberFilterString(string? documentNumber = null)
    {
        if(documentNumber == null)
            return String.Empty;

        return $"and kod eq \"{documentNumber}\"";
    }
    
    private string GetCompanyIdFilterString(string? companyId = null)
    {
        if(companyId == null)
            return String.Empty;

        return $"and ic eq \"code:{companyId}\"";
    }
}