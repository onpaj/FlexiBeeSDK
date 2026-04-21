using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.CashRegisters;

public interface ICashRegisterClient
{
    Task<OperationResult<OperationResultDetail>> UnPairPayment(string paymentCode, CancellationToken cancellationToken = default);
}
