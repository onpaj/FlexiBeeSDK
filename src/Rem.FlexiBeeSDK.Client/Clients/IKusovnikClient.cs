using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IKusovnikClient : IResourceClient<Kusovnik>
    {
        Task<IList<Kusovnik>> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}