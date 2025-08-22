using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Invoices;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices
{
    public class IssuedInvoiceClient : ResourceClient, IIssuedInvoiceClient
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

        protected override string ResourceIdentifier => Agenda.IssuedInvoices;

        public async Task<IssuedInvoiceDetailFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.Items)
                .WithRelation(Relations.References)
                .Build();

           var found = await GetAsync<IssuedInvoiceDetailFlexiDto>(query, cancellationToken: cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(IssuedInvoiceDetailFlexiDto)} with key {code} not found");

           return found.Single();
        }

        public Task<OperationResult<OperationResultDetail>> SaveAsync(IssuedInvoiceDetailFlexiDto invoice,
            CancellationToken cancellationToken = default) => PostAsync(invoice, cancellationToken: cancellationToken);
    }
}