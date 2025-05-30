using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate
{
    public class StockToDateClient : ResourceClient, IStockToDateClient
    {
        public StockToDateClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<StockToDateClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.StockToDate;
        protected override string? RequestIdentifier => null;

        public async Task<IReadOnlyList<Model.Products.StockToDate>> GetAsync(DateTime date, int warehouseId, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
        {
            var queryDoc = new StockToDateRequest()
            {
                Limit = limit,
                Start = skip,
            };

            var query = new FlexiQuery();
            query.Parameters.Add("datum", date.ToString("yyyy-MM-dd"));
            query.Parameters.Add("sklad", warehouseId.ToString());
            var result = await PostAsync<StockToDateRequest, StockToDateResult>(queryDoc, query, cancellationToken: cancellationToken);
            
            return result?.Result?.StockData
                .Select(s => new Model.Products.StockToDate
            {
                ProductCode = s.Product.First().Code,
                ProductName = s.Product.First().Name,
                ProductId = s.Id,
                OnStock = s.Amount,
                Reserved = s.AmountRequired,
                Price = s.AveragePrice,
                ProductTypeId = s.ProductTypeId,
                MoqName = s.Product.First().MoqName,
                MoqAmount = s.Product.First().MoqAmount
            }).ToList() ?? new List<Model.Products.StockToDate>();
        }
    }
}