using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices
{
    public interface IIssuedInvoiceClient
    {
        Task<IssuedInvoiceDetailFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<IssuedInvoiceDetailFlexiDto>> GetAllAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default);

        Task<OperationResult<OperationResultDetail>> SaveAsync(IssuedInvoiceDetailFlexiDto invoice, bool unpairIfNecessary = false, CancellationToken cancellationToken = default);
    }
}