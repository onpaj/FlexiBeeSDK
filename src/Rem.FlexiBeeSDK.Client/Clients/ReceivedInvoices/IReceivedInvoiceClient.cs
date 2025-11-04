using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices
{
    public interface  IReceivedInvoiceClient
    {
        Task<ReceivedInvoiceFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReceivedInvoiceFlexiDto>> SearchAsync(ReceivedInvoiceRequest searchRequest, CancellationToken cancellationToken = default);

        Task<OperationResult<ReceivedInvoiceTagsResult>> AddTagAsync(int invoiceId, string tagCode, CancellationToken cancellationToken = default);
        Task<OperationResult<ReceivedInvoiceTagsResult>> RemoveTagAsync(int invoiceId, string tagCode, CancellationToken cancellationToken = default);

        Task<List<string>> GetTagsAsync(int invoiceId, CancellationToken cancellationToken = default);
    }
}