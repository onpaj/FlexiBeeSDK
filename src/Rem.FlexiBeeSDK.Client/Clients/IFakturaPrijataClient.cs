using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface  IFakturaPrijataClient : IReadOnlyResourceClient<FakturaPrijata>
    {
        Task<FakturaPrijata> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}