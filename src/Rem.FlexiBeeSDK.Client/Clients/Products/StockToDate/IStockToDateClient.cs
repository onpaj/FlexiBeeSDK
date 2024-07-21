using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public interface IStockToDateClient
{
    Task<IReadOnlyList<Model.Products.StockToDate>> GetAsync(DateTime date, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
}