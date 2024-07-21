﻿using System;
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

        public async Task<IReadOnlyList<Model.Products.StockToDate>> GetAsync(DateTime date, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
        {
            var queryDoc = new StockToDateRequest()
            {
                Limit = limit,
                Start = skip,
            };

            var result = await PostAsync<StockToDateRequest, StockToDateResult>(queryDoc, cancellationToken);
            
            return result?.Result?.StockData.Select(s => new Model.Products.StockToDate
            {
                ProductCode = s.Product.First().Code,
                ProductName = s.Product.First().Name,
                OnStock = s.Amount,
                Reserved = s.AmountRequired,
                Price = s.AveragePrice,
                ProductTypeId = s.ProductTypeId
            }).ToList() ?? new List<Model.Products.StockToDate>();
        }
    }
}