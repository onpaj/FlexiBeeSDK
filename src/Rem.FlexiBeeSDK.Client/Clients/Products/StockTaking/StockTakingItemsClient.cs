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

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;

public class StockTakingItemsClient : ResourceClient, IStockTakingItemsClient
{
    public StockTakingItemsClient(
        FlexiBeeSettings connection, 
        IHttpClientFactory httpClientFactory, 
        IResultHandler resultHandler, 
        ILogger<StockTakingItemsClient> logger
        ) : base(connection, httpClientFactory, resultHandler, logger)
    {
    }
    
    protected override string ResourceIdentifier => Agenda.StockTakingItems;

    public async Task<IList<StockTakingItemResult>> GetStockTakingsAsync(int stockTakingId, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder().Raw($"(inventura='{stockTakingId}')")
            .WithCustomDetail("id,lastUpdate,mnozMjReal,mnozMjKarta,sarze,expirace,skladKarta,cenik")
            .Build();
        var result = await GetAsync<StockTakingItemResult>(query, cancellationToken: cancellationToken);

        return result;
    }

    public async Task AddStockTakingsAsync(int stockTakingId, int warehouseId, IEnumerable<AddStockTakingItemRequest> items, CancellationToken cancellationToken = default)
    {
        var itemsList = items.ToList();
        foreach (var i in itemsList)
        {
            if (!i.ProductCode.StartsWith("code:"))
            {
                i.ProductCode = "code:" + i.ProductCode;
            }
            i.StockTakingHeaderId = stockTakingId;
            i.WarehouseId = warehouseId;
        }

        var result = await PostAsync(itemsList, cancellationToken: cancellationToken);
        
        if (!result.IsSuccess)
        {
            throw new InvalidOperationException($"{nameof(AddStockTakingsAsync)} failed ({result.StatusCode}): {result.ErrorMessage}");
        }
    }
}