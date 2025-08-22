using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices
{
    public interface IIssuedInvoiceClient
    {
        Task<IssuedInvoiceDetailFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default);
        
        Task<OperationResult<OperationResultDetail>> SaveAsync(IssuedInvoiceDetailFlexiDto invoice, CancellationToken cancellationToken = default);
    }
}