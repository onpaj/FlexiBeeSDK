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
        
        protected override string? RequestIdentifier => null;

        public async Task<ReceivedInvoiceFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new FlexiQuery();
            var result = await PostAsync<ReceivedInvoiceRequest, ReceivedInvoiceSearchResult>(new ReceivedInvoiceRequest(documentNumber: code), query, cancellationToken: cancellationToken);

           var found = result?.Result?.ReceivedInvoices ?? new List<ReceivedInvoiceFlexiDto>();
           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(ReceivedInvoiceFlexiDto)} with key {code} not found");

           return found.Single();
        }

        public async Task<IReadOnlyList<ReceivedInvoiceFlexiDto>> SearchAsync(ReceivedInvoiceRequest searchRequest, CancellationToken cancellationToken = default)
        {
            var query = new FlexiQuery();
            var result = await PostAsync<ReceivedInvoiceRequest, ReceivedInvoiceSearchResult>(searchRequest, query, cancellationToken: cancellationToken);

            return result?.Result?.ReceivedInvoices ?? new List<ReceivedInvoiceFlexiDto>();
        }
    }
}