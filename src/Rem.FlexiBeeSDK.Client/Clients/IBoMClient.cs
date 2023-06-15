using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IBoMClient : IReadOnlyResourceClient<BoM>
    {
        Task<IList<BoM>> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}