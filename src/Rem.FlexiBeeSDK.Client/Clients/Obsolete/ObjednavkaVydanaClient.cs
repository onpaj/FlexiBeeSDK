using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    [Obsolete]
    public class ObjednavkaVydanaClient : ResourceClient<ObjednavkaVydana>, IObjednavkaVydanaClient
    {
        public ObjednavkaVydanaClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<ObjednavkaVydanaClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => "objednavka-vydana";

        public async Task<ObjednavkaVydana> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.Items)
                .Build();

            var found = await FindAsync(query, cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(ObjednavkaVydana)} with key {code} not found");

            return found.Single();
        }

        public async Task<IList<VazebniDoklad>> GetLinkedItems(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.ReferenceDocs)
                .WithFullDetail()
                .Build();

            var found = await FindAsync(query, cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(ObjednavkaVydana)} with key {code} not found");

            return found.Single().VazebniDoklady;
        }
    }
}