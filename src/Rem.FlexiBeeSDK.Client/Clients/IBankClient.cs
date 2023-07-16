using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Payments;

namespace Rem.FlexiBeeSDK.Client.Clients;

public interface IBankClient : IResourceClient<BankPayment>
{
    Task<OperationResult> UnPairPayment(int paymentId, CancellationToken cancellationToken = default);
}