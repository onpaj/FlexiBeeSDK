using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.BoM;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public interface IBoMClient
    {
        Task<IList<BoMItemFlexiDto>> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<IList<BoMItemFlexiDto>> GetByIngredientAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default);

        Task<ProductWeightFlexiDto?> GetBomWeight(string productCode, CancellationToken cancellationToken = default);
    }
}