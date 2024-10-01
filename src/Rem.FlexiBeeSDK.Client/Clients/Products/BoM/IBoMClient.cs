using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public interface IBoMClient
    {
        Task<IList<Model.BoMItem>> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default);
    }
}