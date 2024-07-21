using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients.Contacts
{
    public interface IContactClient 
    {
        Task<Contact> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<Contact> GetByIdAsync(string ic, CancellationToken cancellationToken = default);
    }
}