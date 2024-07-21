using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients.Contacts
{
    public class ContactClient : ResourceClient, IContactClient
    {
        public ContactClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<ContactClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.Contact;

        public Task<Contact> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            return GetByQueryAsync($"kod='{code}'", cancellationToken);
        }

        public Task<Contact> GetByIdAsync(string ic, CancellationToken cancellationToken = default)
        {
            return GetByQueryAsync($"ic='{ic}'", cancellationToken);
        }

        private async Task<Contact> GetByQueryAsync(string query, CancellationToken cancellationToken = default)
        {
            var q = new QueryBuilder()
                .Raw(query)
                .Build();

            var found = await GetAsync<Contact>(q, cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(Contact)} with query {query} not found");

            return found.Single();
        }
    }
}