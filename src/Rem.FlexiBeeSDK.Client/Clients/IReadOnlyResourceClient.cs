using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IReadOnlyResourceClient<T>
    {
        Task<IList<T>> FindAsync(Query query, CancellationToken cancellationToken = default);
    }
}