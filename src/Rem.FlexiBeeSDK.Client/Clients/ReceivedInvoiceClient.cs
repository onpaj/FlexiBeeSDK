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

        protected override string ResourceIdentifier => Agenda.ReceivedInvoices;

        public async Task<ReceivedInvoice> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"kod='{code}'")
                .WithRelation(Relations.Items)
                .Build();

           var found = await FindAsync(query, cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(ReceivedInvoice)} with key {code} not found");

           return found.Single();
        }
    }
    
    public abstract class UserQueryClient<T> : ResourceClient<T>, IUserQueryClient<T>
    {
        public const string LimitParamName = "limit";
        public UserQueryClient(
            FlexiBeeSettings connection, 
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<ReceivedInvoiceClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }
        protected abstract int QueryId { get; }

        
        protected override string ResourceIdentifier => Agenda.UserQuery;
        protected override string ResultIdentifier => "DotazView";
        public async Task<IList<T>> FindAsync(Dictionary<string, string> queryParameters, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"{QueryId}/call")
                .WithParameters(queryParameters)
                .Build();

            var found = await FindAsync(query, cancellationToken);


            return found;
        }
    }
}