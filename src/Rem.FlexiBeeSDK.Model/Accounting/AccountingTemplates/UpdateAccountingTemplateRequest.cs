using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;

public class UpdateAccountingTemplateRequest
{
    public UpdateAccountingTemplateRequest(string invoiceCode, string? accountingTemplateCode, string? departmentCode)
    {
        Items = new List<UpdateAccountingTemplateRequestItem>()
        {
            new ()
            {
                Code = invoiceCode,
                AccountingTemplateCode = accountingTemplateCode != null ? $"code:{accountingTemplateCode}" : null,
                DepartmentCode = departmentCode != null ? $"code:{departmentCode}" :  null,
            }
        };
    }

    [JsonProperty("faktura-prijata", NullValueHandling = NullValueHandling.Ignore)]
    public List<UpdateAccountingTemplateRequestItem> Items { get; set; }

    [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }
}