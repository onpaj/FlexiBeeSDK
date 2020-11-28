using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class KusovnikClient : ResourceClient<Kusovnik>, IKusovnikClient
    {
        public KusovnikClient(FlexiBeeConnection connection, HttpClient httpClient) 
            : base(connection, httpClient)
        {
        }

        protected override string ResourceIdentifier => "kusovnik";
        public Task<IList<Kusovnik>> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"otecCenik='code:{code}'")
                .Build();

            return FindAsync(query, cancellationToken);
        }
    }
}