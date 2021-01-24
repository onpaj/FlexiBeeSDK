﻿using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface ISkladovyPohybClient : IReadOnlyResourceClient<SkladovyPohyb>
    {
        Task<SkladovyPohyb> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}