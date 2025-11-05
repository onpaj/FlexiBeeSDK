using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Invoices;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices
{
    public class ReceivedInvoiceClient : ResourceClient, IReceivedInvoiceClient
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
        
        protected override string? RequestIdentifier => null;

        public async Task<ReceivedInvoiceFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new FlexiQuery();
            var result = await PostAsync<ReceivedInvoiceRequest, ReceivedInvoiceSearchResult>(new ReceivedInvoiceRequest(documentNumber: code), query, cancellationToken: cancellationToken);

           var found = result?.Result?.ReceivedInvoices ?? new List<ReceivedInvoiceFlexiDto>();
           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(ReceivedInvoiceFlexiDto)} with key {code} not found");

           return found.Single();
        }

        public async Task<IReadOnlyList<ReceivedInvoiceFlexiDto>> SearchAsync(ReceivedInvoiceRequest searchRequest, CancellationToken cancellationToken = default)
        {
            var query = new FlexiQuery();
            var result = await PostAsync<ReceivedInvoiceRequest, ReceivedInvoiceSearchResult>(searchRequest, query, cancellationToken: cancellationToken);

            return result?.Result?.ReceivedInvoices ?? new List<ReceivedInvoiceFlexiDto>();
        }

        public async Task<OperationResult<ReceivedInvoiceTagsResult>> AddTagAsync(string invoiceCode, IEnumerable<string> tagCodes, CancellationToken cancellationToken = default)
        {
            var tags = await GetTagsAsync(invoiceCode, cancellationToken);
            tags.AddRange(tagCodes);
            var result = await SaveTagsAsync(invoiceCode, tags, cancellationToken);
            return result;
        }

        public async Task<OperationResult<ReceivedInvoiceTagsResult>> RemoveTagAsync(string invoiceCode, IEnumerable<string> tagCodes, CancellationToken cancellationToken = default)
        {
            var tags = await GetTagsAsync(invoiceCode, cancellationToken);
            foreach(var tagCode in tagCodes)
                tags.Remove(tagCode);
            var result = await SaveTagsAsync(invoiceCode, tags, cancellationToken);
            return result;
        }

        public async Task<List<string>> GetTagsAsync(string invoiceCode, CancellationToken cancellationToken = default)
        {
            var request = new ReceivedInvoiceTagsRequest(invoiceCode);
            var query = new FlexiQuery();
            var currentTags = 
                await PostAsync<ReceivedInvoiceTagsRequest, ReceivedInvoiceTagsResult>(request, query, cancellationToken: cancellationToken);


            return currentTags.Result?.Invoices.FirstOrDefault()?.Tags ?? new List<string>();
        }
        
        private async Task<OperationResult<ReceivedInvoiceTagsResult>> SaveTagsAsync(string invoiceCode, IEnumerable<string> tags, CancellationToken cancellationToken = default)
        {
            var request = new ReceivedInvoiceSetTagsRequest(invoiceCode, tags);
            var currentTags =
                await PostAsync<ReceivedInvoiceSetTagsRequest, ReceivedInvoiceTagsResult>(request, cancellationToken: cancellationToken);

            return currentTags;
        }
    }

    public class ReceivedInvoiceTagsResult
    {
        [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("faktura-prijata", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReceivedInvoiceTagsDto> Invoices { get; set; }
        
    }

    public class ReceivedInvoiceTagsDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("stitky", NullValueHandling = NullValueHandling.Ignore)]
        public string? TagsRaw { get; set; }
        
        public List<string> Tags => TagsRaw?.Split(',').Select(s => s.Trim()).ToList() ??  new List<string>();
    }

    public class ReceivedInvoiceTagsRequest
    {
        private readonly string _invoiceCode;

        public ReceivedInvoiceTagsRequest(string invoiceCode)
        {
            _invoiceCode = invoiceCode;
        }


        [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

        [JsonProperty("detail")]
        public string Detail { get; set; } = "custom:id,zamekK,stitky,polozkyFaktury(id)";

        [JsonProperty("limit")] public int Limit { get; set; } = 0;

        [JsonProperty("start")] public int Start { get; set; } = 0;

        [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

        [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

        [JsonProperty("@version")] public string Version { get; set; } = "1.0";

        [JsonProperty("filter")] public string Filter => $"kod in (\"{_invoiceCode}\")";

    }
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ReceivedInvoiceSetTagDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("stitky", NullValueHandling = NullValueHandling.Ignore)]
        public string TagsRaw { get; set; }

        [JsonProperty("stitky@removeAll", NullValueHandling = NullValueHandling.Ignore)]
        public bool TagsRemoveAll { get; set; } = true;
    }

    public class ReceivedInvoiceSetTagsRequest
    {
        public ReceivedInvoiceSetTagsRequest(string invoiceCode, IEnumerable<string> tags)
        {
            Invoices = new List<ReceivedInvoiceSetTagDto>()
            {
                new ReceivedInvoiceSetTagDto()
                {
                    Id = $"code:{invoiceCode}",
                    TagsRaw = string.Join(", ", tags)
                }
            };
        }

        [JsonProperty("@atomic", NullValueHandling = NullValueHandling.Ignore)]
        public bool Atomic { get; set; }

        [JsonProperty("faktura-prijata", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReceivedInvoiceSetTagDto> Invoices { get; set; }

        [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }
    }



}