using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;

public class AccountingTemplateFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }
    
    [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }
    
    [JsonProperty("protiUcetVydej", NullValueHandling = NullValueHandling.Ignore)]
    public string AccountCodeRaw { get; set; }
    public string AccountCode => AccountCodeRaw.Replace("code:", "");

    [JsonProperty("modulFav")]
    public bool ModuleIssuedInvoicesAvailable { get; set; }

    [JsonProperty("modulFap")]
    public bool ModuleReceivedInvoicedAvailable { get; set; }

    [JsonProperty("modulPhl")]
    public bool ModuleReceivablesAvailable { get; set; }

    [JsonProperty("modulZav")]
    public bool ModulePayablesAvailable { get; set; }

    [JsonProperty("modulBanP")]
    public bool ModuleBankStatementsReceivedAvailable { get; set; }

    [JsonProperty("modulBanV")]
    public bool ModuleBankStatementsIssuedAvailable { get; set; }

    [JsonProperty("modulPokP")]
    public bool ModuleCashDocumentsReceivedAvailable { get; set; }

    [JsonProperty("modulPokV")]
    public bool ModuleCashDocumentsIssuedAvailable { get; set; }

    [JsonProperty("modulSklP")]
    public bool ModuleWarehouseMovementsReceivedAvailable { get; set; }

    [JsonProperty("modulSklV")]
    public bool ModuleWarehouseMovementsIssuedAvailable { get; set; }

    [JsonProperty("modulInt")]
    public bool ModuleInternalDocumentsAvailable { get; set; }
}