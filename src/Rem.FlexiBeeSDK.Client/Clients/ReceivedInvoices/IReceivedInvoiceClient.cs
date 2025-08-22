using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices
{
    public interface  IReceivedInvoiceClient
    {
        Task<ReceivedInvoiceDetailFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReceivedInvoiceFlexiDto>> SearchAsync(ReceivedInvoiceRequest searchRequest, CancellationToken cancellationToken = default);
    }
}