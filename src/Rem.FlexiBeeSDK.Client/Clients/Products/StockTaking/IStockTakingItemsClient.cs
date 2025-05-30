using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;

public interface IStockTakingItemsClient
{
    Task<IList<StockTakingItemResult>> GetStockTakingsAsync(int headerId, CancellationToken cancellationToken = default);
    Task AddStockTakingsAsync(int headerId, int warehouseId, IEnumerable<AddStockTakingItemRequest> items, CancellationToken cancellationToken = default);
}