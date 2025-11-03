using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting;
using Rem.FlexiBeeSDK.Model.Accounting.AccountingTemplates;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting;

public interface IAccountingTemplateClient
{
    Task<IReadOnlyList<AccountingTemplateFlexiDto>> GetAsync(CancellationToken cancellationToken = default);
    
    Task<OperationResult<OperationResultDetail>> UpdateInvoiceAsync(string invoiceCode, string? accountingTemplateCode, string? departmentCode, CancellationToken cancellationToken = default);
}