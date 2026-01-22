using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public interface IStockMovementClient
{
    /// <summary>
    /// Get stock movement by ID
    /// </summary>
    Task<StockMovementFlexiDto?> GetAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get stock movement by code
    /// </summary>
    Task<StockMovementFlexiDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get stock movements by date range with optional filters
    /// </summary>
    Task<IReadOnlyList<StockMovementFlexiDto>> GetAsync(
        DateTime dateFrom,
        DateTime dateTo,
        StockMovementDirection? direction = null,
        string? warehouseCode = null,
        int? documentTypeId = null,
        int limit = 0,
        int skip = 0,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new stock movement
    /// </summary>
    Task<OperationResult<OperationResultDetail>> SaveAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update existing stock movement
    /// </summary>
    Task<OperationResult<OperationResultDetail>> UpdateAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default);
}
