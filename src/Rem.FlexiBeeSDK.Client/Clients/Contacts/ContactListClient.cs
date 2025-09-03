using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Contacts;
using Rem.FlexiBeeSDK.Model.Products;
using Rem.FlexiBeeSDK.Model.Products.StockToDate;
using ContactFlexiDto = Rem.FlexiBeeSDK.Model.Contacts.ContactFlexiDto;

namespace Rem.FlexiBeeSDK.Client.Clients.Contacts
{
    public class ContactListClient : ResourceClient, IContactListClient
    {
        public ContactListClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<ContactListClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.ContactList;
        protected override string? RequestIdentifier => null;

        public async Task<IReadOnlyList<ContactFlexiDto>> GetAsync(IEnumerable<ContactType> contactTypes, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
        {
            var queryDoc = new ContactListRequest(contactTypes)
            {
                Limit = limit,
                Start = skip,
            };

            var query = new FlexiQuery();
            var result = await PostAsync<ContactListRequest, ContactListResult>(queryDoc, query, cancellationToken: cancellationToken);

            return result?.Result?.Contacts ?? new List<ContactFlexiDto>();
        }
    }
}

