using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Products;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public interface IStockItemsMovementClient
{
    Task<IReadOnlyList<StockItemMovementFlexiDto>> GetAsync(
        DateTime dateFrom, 
        DateTime dateTo, 
        StockMovementDirection direction = StockMovementDirection.Any, 
        string? storeCode = null,
        int? documentTypeId = null,
        string? documentCode = null,
        int limit = 0, 
        int skip = 0, 
        CancellationToken cancellationToken = default);
}