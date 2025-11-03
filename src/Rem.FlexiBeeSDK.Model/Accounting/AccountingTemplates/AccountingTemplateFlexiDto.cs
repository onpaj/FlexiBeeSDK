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
}