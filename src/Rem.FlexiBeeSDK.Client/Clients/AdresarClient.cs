using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class AdresarClient : ResourceClient<Adresar>, IAdresarClient
    {
        public AdresarClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            ILogger<AdresarClient> logger
        )
            : base(connection, httpClientFactory, logger)
        {
        }

        protected override string ResourceIdentifier => "adresar";

        public Task<Adresar> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            return GetByQueryAsync($"kod='{code}'", cancellationToken);
        }

        public Task<Adresar> GetByIcAsync(string ic, CancellationToken cancellationToken = default)
        {
            return GetByQueryAsync($"ic='{ic}'", cancellationToken);
        }

        private async Task<Adresar> GetByQueryAsync(string query, CancellationToken cancellationToken = default)
        {
            var q = new QueryBuilder()
                .Raw(query)
                .Build();

            var found = await FindAsync(q, cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(Adresar)} with query {query} not found");

            return found.Single();
        }
    }
}