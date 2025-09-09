using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;
using Rem.FlexiBeeSDK.Model.Products.Sets;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public interface IProductSetsClient
{
    Task<IReadOnlyList<ProductSetFlexiDto>> GetAsync(string productCode, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
}
