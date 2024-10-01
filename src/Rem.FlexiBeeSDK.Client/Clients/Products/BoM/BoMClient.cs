using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products;

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

        public async Task<IList<Model.BoMItem>> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var bomQuery = new BomRequest(code);
            
           var result = await PostAsync<BomRequest, BomList>(bomQuery, new FlexiQuery(), cancellationToken: cancellationToken);
           return result.Result.BoM;
        }

        public async Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default)
        {
            var document = new RecalculatePriceRequest()
            {
                BomId = bomId
            };

            var result = await PutAsync(document, cancellationToken: cancellationToken);

            return result.IsSuccess;
        }
    }
}