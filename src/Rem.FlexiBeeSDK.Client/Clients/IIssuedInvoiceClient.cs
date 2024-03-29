﻿using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public interface IIssuedInvoiceClient : IResourceClient<IssuedInvoice>
    {
        Task<IssuedInvoice> GetAsync(string code, CancellationToken cancellationToken = default);
    }
}