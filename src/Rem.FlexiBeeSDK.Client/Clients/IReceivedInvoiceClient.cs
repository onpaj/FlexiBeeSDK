using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface  IReceivedInvoiceClient : IResourceClient<ReceivedInvoice>
    {
        Task<ReceivedInvoice> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}