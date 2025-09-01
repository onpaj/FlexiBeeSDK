using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Contacts;
using Rem.FlexiBeeSDK.Model.Response;

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

        protected override string ResourceIdentifier => Agenda.ContactList;

        public Task<ContactFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            return GetByQueryAsync(new QueryBuilder().ByCode(code).Build(), cancellationToken);
        }

        public Task<ContactFlexiDto> GetByIdAsync(string ic, CancellationToken cancellationToken = default)
        {
            return GetByQueryAsync(new QueryBuilder().ByProperty("ic", ic).Build(), cancellationToken);
        }

        private async Task<ContactFlexiDto> GetByQueryAsync(Query query, CancellationToken cancellationToken = default)
        {
            var found = await GetAsync<ContactFlexiDto>(query, cancellationToken: cancellationToken);

            if (!found.Any())
                throw new KeyNotFoundException($"Entity {nameof(ContactFlexiDto)} with query {query} not found");

            return found.Single();
        }
        
        public Task<OperationResult<OperationResultDetail>> UpdateAsync(ContactFlexiDto contact, CancellationToken cancellationToken = default)
        {
            return PostAsync<ContactFlexiDto[]>([contact], cancellationToken: cancellationToken);
        }
    }
}