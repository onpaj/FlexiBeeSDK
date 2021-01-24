using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IObjednavkaVydanaClient : IReadOnlyResourceClient<ObjednavkaVydana>
    {
        Task<ObjednavkaVydana> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<IList<VazebniDoklad>> GetLinkedItems(string code, CancellationToken cancellationToken = default);
    }
}