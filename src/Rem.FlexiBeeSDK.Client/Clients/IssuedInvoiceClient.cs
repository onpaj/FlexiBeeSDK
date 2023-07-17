using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class IssuedInvoiceClient : ResourceClient<IssuedInvoice>, IIssuedInvoiceClient
    {
        public IssuedInvoiceClient(
            FlexiBeeSettings connection, 
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<IssuedInvoiceClient> logger
            )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Evidence.IssuedInvoices;

        public async Task<IssuedInvoice> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.Items)
                .WithRelation(Relations.References)
                .Build();

           var found = await FindAsync(query, cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(IssuedInvoice)} with key {code} not found");

           return found.Single();
        }
    }
}