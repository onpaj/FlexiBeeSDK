using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class FakturaPrijataClient : ResourceClient<FakturaPrijata>, IFakturaPrijataClient
    {
        public FakturaPrijataClient(
            FlexiBeeSettings connection, 
            IHttpClientFactory httpClientFactory,
            ILogger<FakturaPrijataClient> logger
            )
            : base(connection, httpClientFactory, logger)
        {
        }

        protected override string ResourceIdentifier => "faktura-prijata";

        public async Task<FakturaPrijata> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.PolozkyDokladu)
                .Build();

           var found = await FindAsync(query, cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(FakturaPrijata)} with key {code} not found");

           return found.Single();
        }
    }

    public class SkladovyPohybClient : ResourceClient<SkladovyPohyb>, ISkladovyPohybClient
    {
        public SkladovyPohybClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            ILogger<SkladovyPohybClient> logger
        )
            : base(connection, httpClientFactory, logger)
        {
        }

        protected override string ResourceIdentifier => "skladovy-pohyb";

        public async Task<SkladovyPohyb> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .ByCode(code)
                .WithRelation(Relations.PolozkyDokladu)
                .WithFullDetail()
                .Build();

            var found = await FindAsync(query, cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(SkladovyPohyb)} with key {code} not found");

            return found.Single();
        }
    }
}