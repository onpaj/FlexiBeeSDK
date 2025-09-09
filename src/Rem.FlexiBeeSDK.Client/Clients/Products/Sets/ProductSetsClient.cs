using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;
using Rem.FlexiBeeSDK.Model.Products.Sets;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public class ProductSetsClient : ResourceClient, IProductSetsClient
{
    public ProductSetsClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler  resultHandler,
        ILogger<LedgerClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.Sets;
    protected override string? RequestIdentifier => null;

    public async Task<IReadOnlyList<ProductSetFlexiDto>> GetAsync(string productCode, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
    {
        var queryDoc = new ProductSetsRequest(productCode)
        {
            Limit = limit,
            Start = skip,
        };

        var query = new FlexiQuery();
        var result = await PostAsync<ProductSetsRequest, ProductSetsResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.Sets ?? new List<ProductSetFlexiDto>();
    }
}