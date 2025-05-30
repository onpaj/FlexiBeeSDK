using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.StockTaking;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;

public class StockTakingClient : ResourceClient, IStockTakingClient
{
    private readonly IStockTakingItemsClient _stockTakingItemsClient;

    public StockTakingClient(
        FlexiBeeSettings connection, 
        IHttpClientFactory httpClientFactory, 
        IResultHandler resultHandler, 
        IStockTakingItemsClient stockTakingItemsClient,
        ILogger<StockTakingClient> logger) 
        : base(connection, httpClientFactory, resultHandler, logger)
    {
        _stockTakingItemsClient = stockTakingItemsClient;
    }

    public async Task<StockTakingHeader> CreateHeaderAsync(StockTakingHeaderRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsync<StockTakingHeaderRequest, OperationResultDetail>(request, cancellationToken: cancellationToken);
        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.ErrorMessage);
        }
        
        var id = Convert.ToInt32(result?.Result?.Results?.FirstOrDefault()?.Id);
        return await GetHeaderAsync(id, cancellationToken);
    }


    public async Task<StockTakingHeader> GetHeaderAsync(int id, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder()
            .ById(id)
            .WithFullDetail()
            .Build();
        var headers = await GetAsync<StockTakingHeader>(query, cancellationToken: cancellationToken);
        return headers.FirstOrDefault() ?? throw new KeyNotFoundException($"StockTakingHeader with ID {id} not found.");
    }
    
    
    public async Task AddMissingLotsAsync(int stockTakingId, IEnumerable<int> productIds, CancellationToken cancellationToken = default)
    {
        var request = new FilterRequest("id", productIds.Select(s => s.ToString()));
        var result = await PostAsync<FilterRequest, OperationResultDetail>(request, cancellationToken: cancellationToken, customResourceIdentifier: $"{ResourceIdentifier}/{stockTakingId}/vloz-polozky.json");

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException($"Unable to {nameof(AddMissingLotsAsync)} to header {stockTakingId} ({result.StatusCode}): {result.ErrorMessage}");
        }
    }

    public async Task RecomputeAsync(int stockTakingId, CancellationToken cancellationToken = default)
    {
        await GetCustomAsync($"{ResourceIdentifier}/{stockTakingId}/aktualizuj-stavy.json", cancellationToken);
    }

    public async Task SubmitAsync(int stockTakingId, int documentTypeId, CancellationToken cancellationToken = default)
    {
        var result = await GetCustomAsync<WinstromEnvelope<OperationResultDetail>>($"{ResourceIdentifier}/{stockTakingId}/vygeneruj-doklady.json?typDoklId={documentTypeId}", cancellationToken);
        if (!result.Data.IsSuccess)
        {
            throw new InvalidOperationException($"Unable to {nameof(SubmitAsync)} to header {stockTakingId}: {result.Data.Message}");
        }
    }

    protected override string ResourceIdentifier => Agenda.StockTakings;
}