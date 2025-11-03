using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices;

public class ReceivedInvoiceRequest
{
    public ReceivedInvoiceRequest(DateTime? dateFrom = null, DateTime? dateTo = null, string? label = null, string? accountingTemplate = null, string? documentNumber = null, string? companyId = null)
    {
        var filters = new List<string>();
        
        if (dateFrom.HasValue && dateTo.HasValue)
        {
            filters.Add($"(datVyst gte \"{dateFrom.Value:yyyy-MM-dd}\" and datVyst lte \"{dateTo.Value:yyyy-MM-dd}\")");
        }
        
        var labelFilter = GetLabelFilterString(label);
        if (!string.IsNullOrEmpty(labelFilter))
            filters.Add(labelFilter);
            
        var accountingTemplateFilter = GetAccountingTemplateFilterString(accountingTemplate);
        if (!string.IsNullOrEmpty(accountingTemplateFilter))
            filters.Add(accountingTemplateFilter);
            
        var documentNumberFilter = GetDocumentNumberFilterString(documentNumber);
        if (!string.IsNullOrEmpty(documentNumberFilter))
            filters.Add(documentNumberFilter);
            
        var companyIdFilter = GetCompanyIdFilterString(companyId);
        if (!string.IsNullOrEmpty(companyIdFilter))
            filters.Add(companyIdFilter);
        
        Filter = filters.Count > 0 ? $"({string.Join(" and ", filters)})" : string.Empty;
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

        return $"stitky eq \"code:{label}\"";
    }
    
    private string GetAccountingTemplateFilterString(string? accountingTemplate = null)
    {
        if(accountingTemplate == null)
            return String.Empty;

        return $"typUcOp.kod eq \"{accountingTemplate}\"";
    }
    
    private string GetDocumentNumberFilterString(string? documentNumber = null)
    {
        if(documentNumber == null)
            return String.Empty;

        return $"kod eq \"{documentNumber}\"";
    }
    
    private string GetCompanyIdFilterString(string? companyId = null)
    {
        if(companyId == null)
            return String.Empty;

        return $"ic eq \"code:{companyId}\"";
    }
}