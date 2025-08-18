using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.Ledger;

public class LedgerItem
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("parSymbol")]
    public string ParSymbol { get; set; }

    [JsonProperty("datVyst")]
    public DateTime IssueDate { get; set; }

    [JsonProperty("datUcto")]
    public DateTime AccountingDate { get; set; }

    [JsonProperty("doklad")]
    public string Document { get; set; }

    [JsonProperty("nazFirmy")]
    public string CompanyName { get; set; }

    [JsonProperty("stredisko@evidencePath")]
    public string DepartmentEvidencePath { get; set; }

    [JsonProperty("stredisko@internalId")]
    public int DepartmentInternalId { get; set; }

    [JsonProperty("stredisko@ref")]
    public string DepartmentRef { get; set; }

    [JsonProperty("stredisko@showAs")]
    public string DepartmentShowAs { get; set; }

    [JsonProperty("stredisko")]
    public List<Department> Department { get; set; }

    [JsonProperty("popis")]
    public string Description { get; set; }

    [JsonProperty("sumTuz")]
    public double AmountLocal { get; set; }

    [JsonProperty("sumMen")]
    public double AmountForeign { get; set; }

    [JsonProperty("mena@evidencePath")]
    public string CurrencyEvidencePath { get; set; }

    [JsonProperty("mena@internalId")]
    public int CurrencyInternalId { get; set; }

    [JsonProperty("mena@ref")]
    public string CurrencyRef { get; set; }

    [JsonProperty("mena@showAs")]
    public string CurrencyShowAs { get; set; }

    [JsonProperty("mena")]
    public List<Currency> Currency { get; set; }

    [JsonProperty("kurz")]
    public double ExchangeRate { get; set; }

    [JsonProperty("mdUcet@evidencePath")]
    public string DebitAccountEvidencePath { get; set; }

    [JsonProperty("mdUcet@internalId")]
    public int DebitAccountInternalId { get; set; }

    [JsonProperty("mdUcet@ref")]
    public string DebitAccountRef { get; set; }

    [JsonProperty("mdUcet@showAs")]
    public string DebitAccountShowAs { get; set; }

    [JsonProperty("mdUcet")]
    public List<Account> DebitAccountList { get; set; }
    
    public Account? DebitAccount => DebitAccountList.FirstOrDefault(); 

    [JsonProperty("dalUcet@evidencePath")]
    public string CreditAccountEvidencePath { get; set; }

    [JsonProperty("dalUcet@internalId")]
    public int CreditAccountInternalId { get; set; }

    [JsonProperty("dalUcet@ref")]
    public string CreditAccountRef { get; set; }

    [JsonProperty("dalUcet@showAs")]
    public string CreditAccountShowAs { get; set; }

    [JsonProperty("dalUcet")]
    public List<Account> CreditAccountList { get; set; }
    
    public Account? CreditAccount => CreditAccountList.FirstOrDefault(); 

    [JsonProperty("zuctovano")]
    public bool IsCleared { get; set; }

    [JsonProperty("idUcetniDenik")]
    public object JournalId { get; set; }

    [JsonProperty("idDokl")]
    public int DocumentId { get; set; }

    [JsonProperty("idDokl@evidencePath")]
    public string DocumentIdEvidencePath { get; set; }
}