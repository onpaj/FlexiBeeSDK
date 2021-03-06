﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class KusovnikClient : ResourceClient<Kusovnik>, IKusovnikClient
    {
        public KusovnikClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            ILogger<KusovnikClient> logger
        )
            : base(connection, httpClientFactory, logger)
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