using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.IssuedOrders;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedOrders;

public interface IIssuedOrdersClient
{
    Task<IReadOnlyList<IssuedOrderFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, int? documentTypeId = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
    
    Task<IssuedOrderFlexiDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    
    Task<IssuedOrderFlexiDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    
    Task<OperationResult<OperationResultDetail>> SaveAsync(CreateIssuedOrderFlexiDto issuedOrder, CancellationToken cancellationToken = default);
}