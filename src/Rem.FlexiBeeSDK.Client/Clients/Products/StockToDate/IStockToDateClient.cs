using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Products;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public interface IStockToDateClient
{
    Task<IReadOnlyList<StockToDateSummary>> GetAsync(DateTime date, int warehouseId, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
}