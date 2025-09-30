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

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public class BoMClient : ResourceClient, IBoMClient
    {
        public BoMClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<BoMClient> logger
        )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
        }

        protected override string ResourceIdentifier => Agenda.BoM;
        protected override string? RequestIdentifier => null;

        public async Task<IList<Model.BoMItemFlexiDto>> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var queryDoc = new BomRequest().FindByParentCode(code);

            var query = new FlexiQuery();
            var result = await PostAsync<BomRequest, BomResult>(queryDoc, query, cancellationToken: cancellationToken);

            return result?.Result?.BoMItems ?? new List<BoMItemFlexiDto>();
        }

        public async Task<IList<Model.BoMItemFlexiDto>> GetByIngredientAsync(string code, CancellationToken cancellationToken = default)
        {
            var queryDoc = new BomRequest().FindByIngredientCode(code);

            var query = new FlexiQuery();
            var result = await PostAsync<BomRequest, BomResult>(queryDoc, query, cancellationToken: cancellationToken);

            return result?.Result?.BoMItems ?? new List<BoMItemFlexiDto>();
        }
        
        public async Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default)
        {
            var document = new Dictionary<string, object>()
            {
                { ResourceIdentifier, new RecalculatePriceRequest()
                    {
                        BomId = bomId
                    } 
                }
            };

            var result = await PutAsync(document, cancellationToken: cancellationToken);

            return result.IsSuccess;
        }
        
        
        public async Task<ProductWeightFlexiDto?> GetBomWeight(string productCode, CancellationToken cancellationToken = default)
        {
           var bom = await GetAsync(productCode, cancellationToken: cancellationToken);

           if (!bom.Any())
               return null;
           
           var netWeight = bom.Where(w => w.Level == 2 && w.IngredientCode.Substring(0, 6) == productCode.Substring(0, 6))
               .Sum(s => s.Amount); // Material only
           var grossWeight = bom.Where(w => w.Level == 2).Sum(s => s.Amount);

           return new ProductWeightFlexiDto()
           {
               ProductCode = productCode,
               NetWeight = netWeight > 0 ? netWeight : grossWeight,
               Amount = bom.Where(w => w.Level == 1).Sum(s => s.Amount)
           };
        }
    }
}