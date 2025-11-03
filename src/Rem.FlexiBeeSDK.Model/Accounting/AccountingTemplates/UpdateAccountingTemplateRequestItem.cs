using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;

public class UpdateAccountingTemplateRequestItem
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id => $"code:{Code}";

    [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
    public string Code { get; set; }

    [JsonProperty("stredisko", NullValueHandling = NullValueHandling.Ignore)]
    public string? DepartmentCode { get; set; }

    [JsonProperty("typUcOp", NullValueHandling = NullValueHandling.Ignore)]
    public string? AccountingTemplateCode { get; set; }
}