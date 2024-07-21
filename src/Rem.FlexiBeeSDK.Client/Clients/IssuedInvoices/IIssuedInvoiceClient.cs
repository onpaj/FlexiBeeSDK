using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices
{
    public interface IIssuedInvoiceClient
    {
        Task<IssuedInvoice> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}