using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Products.Lots;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate
{
    public interface ILotsClient
    {
        Task<IReadOnlyList<ProductLot>> GetAsync(string? productCode = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
    }
}

