using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public class StockMovementClient : ResourceClient, IStockMovementClient
{
    public StockMovementClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<StockMovementClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.StockMovement;
    protected override string? RequestIdentifier => null;

    public async Task<StockMovementFlexiDto?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var request = new StockMovementRequest(id);
        var query = new FlexiQuery();
        var result = await PostAsync<StockMovementRequest, StockMovementResult>(
            request,
            query,
            cancellationToken: cancellationToken);

        return result?.Result?.StockMovements?.FirstOrDefault();
    }

    public async Task<StockMovementFlexiDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var request = new StockMovementRequest(code);
        var query = new FlexiQuery();
        var result = await PostAsync<StockMovementRequest, StockMovementResult>(
            request,
            query,
            cancellationToken: cancellationToken);

        return result?.Result?.StockMovements?.FirstOrDefault();
    }

    public async Task<IReadOnlyList<StockMovementFlexiDto>> GetAsync(
        DateTime dateFrom,
        DateTime dateTo,
        StockMovementDirection? direction = null,
        string? warehouseCode = null,
        int? documentTypeId = null,
        int limit = 0,
        int skip = 0,
        CancellationToken cancellationToken = default)
    {
        var request = new StockMovementRequest(dateFrom, dateTo, direction, warehouseCode, documentTypeId)
        {
            Limit = limit,
            Start = skip
        };

        var query = new FlexiQuery();
        var result = await PostAsync<StockMovementRequest, StockMovementResult>(
            request,
            query,
            cancellationToken: cancellationToken);

        return result?.Result?.StockMovements ?? new List<StockMovementFlexiDto>();
    }

    public Task<OperationResult<OperationResultDetail>> SaveAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default)
    {
        var envelope = new CreateStockMovementEnvelopeFlexiDto(stockMovement);
        return PostAsync(envelope, cancellationToken: cancellationToken);
    }

    public Task<OperationResult<OperationResultDetail>> UpdateAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default)
    {
        var envelope = new CreateStockMovementEnvelopeFlexiDto(stockMovement);
        return PostAsync(envelope, cancellationToken: cancellationToken);
    }
}
