using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;

public interface IStockTakingClient
{
    Task<StockTakingHeader> GetHeaderAsync(int headerId, CancellationToken cancellationToken = default);

    Task<StockTakingHeader> CreateHeaderAsync(StockTakingHeaderRequest request, CancellationToken cancellationToken = default);
    
    Task AddMissingLotsAsync(int headerId, IEnumerable<int> ids, CancellationToken cancellationToken = default);
    
    Task RecomputeAsync(int headerId, CancellationToken cancellationToken = default);
    
    Task SubmitAsync(int headerId, int documentTypeId, CancellationToken cancellationToken = default);
}