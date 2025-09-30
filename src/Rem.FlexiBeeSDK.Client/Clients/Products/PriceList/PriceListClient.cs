using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products;
using Rem.FlexiBeeSDK.Model.Products.BoM;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public class PriceListClient : ResourceClient, IPriceListClient
    {
        public PriceListClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<PriceListClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.PriceList;
        protected override string? RequestIdentifier => null;

        public Task<OperationResult<OperationResultDetail>> SaveAsync(PriceListFlexiDto priceListData, CancellationToken cancellationToken = default)
            => PostAsync(new PriceListCollectionFlexiDto(priceListData), cancellationToken: cancellationToken);
    }
}