using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.BankAccounts;

public interface IBankAccountClient
{
    Task<OperationResult<OperationResultDetail>> ImportStatement(int flexibeeAccountId, string data);
}