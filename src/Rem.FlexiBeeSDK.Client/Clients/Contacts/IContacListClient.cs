using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Contacts;
using Rem.FlexiBeeSDK.Model.Products;

namespace Rem.FlexiBeeSDK.Client.Clients.Contacts;

public interface IContactListClient
{
    Task<IReadOnlyList<ContactFlexiDto>> GetAsync(IEnumerable<ContactType> contactTypes, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
}