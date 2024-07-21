using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices
{
    public interface  IReceivedInvoiceClient
    {
        Task<ReceivedInvoice> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}