using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;

public class AccountingTemplateResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("predpis-zauctovani")]
    public List<AccountingTemplateFlexiDto> AccountingTemplates { get; set; } = new ();
}