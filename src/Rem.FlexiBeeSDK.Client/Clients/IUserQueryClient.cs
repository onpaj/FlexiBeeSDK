using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients;

public interface IUserQueryClient<T>
{
    Task<IList<T>> FindAsync(Dictionary<string, string> queryParameters, CancellationToken cancellationToken = default);
}