using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IFakturaVydanaClient : IResourceClient<FakturaVydana>
    {
        Task<FakturaVydana> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}