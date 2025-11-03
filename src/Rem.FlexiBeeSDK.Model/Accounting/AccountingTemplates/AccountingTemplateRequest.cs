using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;

public class AccountingTemplateRequest
{
    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:id,kod,nazev,popis,poznam,protiUcetVydej(kod)";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } ="";

    [JsonProperty("order")] public string Order { get; set; } = "id";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";

    [JsonProperty("filter")] public string Filter { get; private set; } = "";
}