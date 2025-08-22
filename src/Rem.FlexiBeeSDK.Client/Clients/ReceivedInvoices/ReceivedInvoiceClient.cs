using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices
{
    public class ReceivedInvoiceClient : ResourceClient, IReceivedInvoiceClient
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

        protected override string ResourceIdentifier => Agenda.ReceivedInvoices;

        public async Task<ReceivedInvoiceDetailFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"(kod='{code}')")
                .WithRelation(Relations.Items)
                .Build();

           var found = await GetAsync<ReceivedInvoiceDetailFlexiDto>(query, cancellationToken: cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(ReceivedInvoiceDetailFlexiDto)} with key {code} not found");

           return found.Single();
        }

        public async Task<IReadOnlyList<ReceivedInvoiceSearchDto>> SearchAsync(ReceivedInvoiceRequest searchRequest, CancellationToken cancellationToken = default)
        {
            var query = new FlexiQuery();
            var result = await PostAsync<ReceivedInvoiceRequest, ReceivedInvoiceSearchResult>(searchRequest, query, cancellationToken: cancellationToken);

            return result?.Result?.ReceivedInvoices ?? new List<ReceivedInvoiceSearchDto>();
        }
    }
}