using System.IO;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients;

public interface IBankAccountClient
{
    Task<OperationResult> ImportStatement(int flexibeeAccountId, string data);
}