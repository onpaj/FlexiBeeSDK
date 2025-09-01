using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Contacts;

namespace Rem.FlexiBeeSDK.Client.Clients.Contacts
{
    public interface IContactClient 
    {
        Task<ContactFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<ContactFlexiDto> GetByIdAsync(string ic, CancellationToken cancellationToken = default);
    }
}