using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Products.StockToDate;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement
{
    public class StockItemsMovementClient : ResourceClient, IStockItemsMovementClient
    {
        public StockItemsMovementClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<StockToDateClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.StockMovements;
        protected override string? RequestIdentifier => null;

        public async Task<IReadOnlyList<StockItemMovementFlexiDto>> GetAsync(
            DateTime dateFrom, 
            DateTime dateTo, 
            StockMovementDirection direction = StockMovementDirection.Any, 
            string? storeCode = null,
            int? documentTypeId = null,
            string? documentCode = null,
            int limit = 0, 
            int skip = 0, 
            CancellationToken cancellationToken = default)
        {
            var queryDoc = new StockItemMovementRequest(
                dateFrom, 
                dateTo, 
                direction,
                storeCode,
                documentTypeId,
                documentCode
                )
            {
                Limit = limit,
                Start = skip,
            };

            var query = new FlexiQuery();
            var result = await PostAsync<StockItemMovementRequest, StockItemsMovementResult>(queryDoc, query, cancellationToken: cancellationToken);

            return result?.Result?.StockItemMovements ?? new List<StockItemMovementFlexiDto>();
        }
        
        
        public Task<OperationResult<OperationResultDetail>> SaveAsync(StockItemsMovementUpsertRequestFlexiDto stockMovementRequest, CancellationToken cancellationToken = default)
            => PostAsync(new StockItemsMovementUpsertRequestEnvelopeFlexiDto(stockMovementRequest), cancellationToken: cancellationToken);
    }
}