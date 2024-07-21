using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients.UserQueries;

public interface IUserQueryClient<T>
{
    Task<IList<T>> GetAsync(Dictionary<string, string> queryParameters, CancellationToken cancellationToken = default);
}