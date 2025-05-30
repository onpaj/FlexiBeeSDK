using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products;
using Rem.FlexiBeeSDK.Model.Products.Lots;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate
{
    public class LotsClient : ResourceClient, ILotsClient
    {
        public LotsClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<LotsClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.Lots;
        protected override string? RequestIdentifier => null;

        public async Task<IReadOnlyList<ProductLot>> GetAsync(string? productCode = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
        {
            var builder = new QueryBuilder();
            if(productCode != null)
                builder.Raw($"(cenik.kod='{productCode}')");
    
            var query = builder
                .WithCustomDetail("id,pocet,sarze,expirace,cenik(kod)")
                .WithLimit(limit)
                .Build();

            var found = await GetAsync<LotsItem>(query, cancellationToken: cancellationToken);

            if(!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(LotsItem)} not found");

            return found.Select(s => new ProductLot()
            {
                Amount = s.Amount,
                Lot = s.Lot,
                Expiration = s.Expiration,
                ProductCode = s.ProductCode.Replace("code:", "")
            }).ToList();
        }
    }
}