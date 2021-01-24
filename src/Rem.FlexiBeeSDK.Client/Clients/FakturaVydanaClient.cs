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
    public class FakturaVydanaClient : ResourceClient<FakturaVydana>, IFakturaVydanaClient
    {
        public FakturaVydanaClient(
            FlexiBeeSettings connection, 
            IHttpClientFactory httpClientFactory,
            ILogger<FakturaVydanaClient> logger
            )
            : base(connection, httpClientFactory, logger)
        {
        }

        protected override string ResourceIdentifier => "faktura-vydana";

        public async Task<FakturaVydana> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.PolozkyDokladu)
                .Build();

           var found = await FindAsync(query, cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(FakturaVydana)} with key {code} not found");

           return found.Single();
        }
    }
}