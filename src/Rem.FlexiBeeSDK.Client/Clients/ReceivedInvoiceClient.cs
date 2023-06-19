using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class ReceivedInvoiceClient : ResourceClient<ReceivedInvoice>, IReceivedInvoiceClient
    {
        public ReceivedInvoiceClient(
            FlexiBeeSettings connection, 
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<ReceivedInvoiceClient> logger
            )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => "faktura-prijata";

        public async Task<ReceivedInvoice> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.PolozkyDokladu)
                .Build();

           var found = await FindAsync(query, cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(ReceivedInvoice)} with key {code} not found");

           return found.Single();
        }
    }
}