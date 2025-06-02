using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products;
using Rem.FlexiBeeSDK.Model.Products.BoM;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public class BoMClient : ResourceClient, IBoMClient
    {
        public BoMClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<BoMClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.BoM;
        protected override string? RequestIdentifier => null;

        public async Task<IList<Model.BoMItemV2>> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .WithNoLimit()
                .WithFullDetail()
                .Raw($"(otecCenik=\"code:{code}\")")
                .Build();
            
            var result = await GetAsync<BoMItemV2>(query, cancellationToken: cancellationToken);

            return result;
        }

        public async Task<IList<Model.BoMItemV2>> GetByIngredientAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .WithNoLimit()
                .WithFullDetail()
                .Raw($"(cenik=\"code:{code}\")")
                .Build();
            
            var result = await GetAsync<BoMItemV2>(query, cancellationToken: cancellationToken);

            return result;
        }
        
        public async Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default)
        {
            var document = new Dictionary<string, object>()
            {
                { ResourceIdentifier, new RecalculatePriceRequest()
                    {
                        BomId = bomId
                    } 
                }
            };

            var result = await PutAsync(document, cancellationToken: cancellationToken);

            return result.IsSuccess;
        }
    }
}