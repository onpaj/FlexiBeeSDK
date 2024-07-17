using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Stock;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class BoMClient : ResourceClient<BoM>, IBoMClient
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
        public Task<IList<BoM>> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"otecCenik='code:{code}'")
                .Build();

            return FindAsync(query, cancellationToken);
        }

        public async Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default)
        {
            var document = new RecalculatePriceRequest(bomId);

            var result = await PutAsync(document, cancellationToken);

            return result.IsSuccess;
        }
    }

    
}