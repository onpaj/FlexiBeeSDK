using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public interface IBoMClient
    {
        Task<IList<Model.BoMItemV2>> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<IList<Model.BoMItemV2>> GetByIngredientAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default);
    }
}