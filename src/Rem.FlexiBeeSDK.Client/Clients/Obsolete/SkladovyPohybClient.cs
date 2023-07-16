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
    public class SkladovyPohybClient : ResourceClient<SkladovyPohyb>, ISkladovyPohybClient
    {
        public SkladovyPohybClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<SkladovyPohybClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => "skladovy-pohyb";

        public async Task<SkladovyPohyb> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .ByCode(code)
                .WithRelation(Relations.Items)
                .WithFullDetail()
                .Build();

            var found = await FindAsync(query, cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(SkladovyPohyb)} with key {code} not found");

            return found.Single();
        }
    }
}