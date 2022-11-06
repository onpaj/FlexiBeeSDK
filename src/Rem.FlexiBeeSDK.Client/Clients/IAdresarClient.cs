using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IAdresarClient : IReadOnlyResourceClient<Adresar>
    {
        Task<Adresar> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<Adresar> GetByIcAsync(string ic, CancellationToken cancellationToken = default);
    }
}