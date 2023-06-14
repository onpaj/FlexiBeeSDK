using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public abstract class ResourceClient<TEntity> : IResourceClient<TEntity>
    {
        private readonly FlexiBeeSettings _connection;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        protected ResourceClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            ILogger logger)
        {
            _connection = connection;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        private string Encode(string connectionLogin, string connectionPassword)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes($"{connectionLogin}:{connectionPassword}");
            return System.Convert.ToBase64String(bytes);
        }

        public virtual async Task<IList<TEntity>> FindAsync(Query query, CancellationToken cancellationToken = default)
        {
            var uri = GetUri(query);
            var client = GetClient();

            _logger.LogDebug($"HttpRequest: GET {uri}");
            var result = await client.GetAsync(uri, cancellationToken);
            _logger.LogDebug($"HttpResult: {result.StatusCode}");
            result.EnsureSuccessStatusCode();

            var json = await result.Content.ReadAsStringAsync();

            JObject jo = JObject.Parse(json);
            JArray list = (JArray)jo.SelectToken($"winstrom.{ResourceIdentifier}");

            return list.ToObject<List<TEntity>>();
        }

        public virtual async Task<OperationResult> SaveAsync(TEntity document, CancellationToken cancellationToken = default)
        {
            var uri = GetUri(document);
            var client = GetClient();

            _logger.LogDebug($"HttpRequest: POST {uri}");


            var json = JsonConvert.SerializeObject(new
            {
                winstrom = new Dictionary<string, object>()
                {
                    { ResourceIdentifier, new []
                    {
                        document
                    }}
                }
            });
            _logger.LogTrace(json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(uri, content, cancellationToken);
            _logger.LogDebug($"HttpResult: {result.StatusCode}");

            var resultContent = await result.Content.ReadAsStringAsync(cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                _logger.LogError(resultContent);
            }
            return new OperationResult(result.StatusCode, resultContent);
        }



        protected virtual Uri GetUri(Query query)
        {
            return new Uri($"{_connection.Server}/c/{_connection.Company}/{ResourceIdentifier}/{query}");
        }

        protected virtual Uri GetUri(TEntity document = default)
        {
            return new Uri($"{_connection.Server}/c/{_connection.Company}/{ResourceIdentifier}");
        }

        protected abstract string ResourceIdentifier { get; }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient(this.GetType().Name);
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Encode(_connection.Login, _connection.Password));

            return client;
        }

       
    }
}