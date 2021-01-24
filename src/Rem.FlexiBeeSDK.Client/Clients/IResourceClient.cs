using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IResourceClient<T> : IReadOnlyResourceClient<T>
    {
        Task<OperationResult> SaveAsync(T document, CancellationToken cancellationToken = default);
    }
}