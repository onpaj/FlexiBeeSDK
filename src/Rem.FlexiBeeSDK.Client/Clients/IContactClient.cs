using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IContactClient : IReadOnlyResourceClient<Contact>
    {
        Task<Contact> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<Contact> GetByIcAsync(string ic, CancellationToken cancellationToken = default);
    }
}