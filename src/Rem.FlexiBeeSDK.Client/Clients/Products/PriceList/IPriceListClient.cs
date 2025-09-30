using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public interface IPriceListClient
    {
        Task<OperationResult<OperationResultDetail>> SaveAsync(PriceListFlexiDto priceListData, CancellationToken cancellationToken = default);
    }
}



// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);