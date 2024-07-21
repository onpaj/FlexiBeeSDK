using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Banks;

public interface IBankClient
{
    Task<OperationResult<OperationResultDetail>> UnPairPayment(int paymentId, CancellationToken cancellationToken = default);
}