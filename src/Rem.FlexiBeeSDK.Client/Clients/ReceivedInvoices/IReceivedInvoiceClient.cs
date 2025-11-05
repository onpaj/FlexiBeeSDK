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

        Task<OperationResult<ReceivedInvoiceTagsResult>> AddTagAsync(string invoiceId, IEnumerable<string> tagCodes, CancellationToken cancellationToken = default);
        Task<OperationResult<ReceivedInvoiceTagsResult>> RemoveTagAsync(string invoiceId, IEnumerable<string> tagCodes, CancellationToken cancellationToken = default);

        Task<List<string>> GetTagsAsync(string invoiceId, CancellationToken cancellationToken = default);
    }

    public static class ReceiverInvoiceClientExtensions
    {
        public static Task<OperationResult<ReceivedInvoiceTagsResult>> AddTagAsync(this IReceivedInvoiceClient client, string invoiceId, string tagCode, CancellationToken cancellationToken = default) =>
            client.AddTagAsync(invoiceId, [tagCode], cancellationToken);
            
        public static Task<OperationResult<ReceivedInvoiceTagsResult>> RemoveTagAsync(this IReceivedInvoiceClient client, string invoiceId, string tagCode, CancellationToken cancellationToken = default) => 
            client.RemoveTagAsync(invoiceId, [tagCode], cancellationToken);
        
    }
}