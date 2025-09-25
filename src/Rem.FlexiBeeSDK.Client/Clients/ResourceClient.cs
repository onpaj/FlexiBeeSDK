using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public abstract class ResourceClient
    {
        private readonly FlexiBeeSettings _connection;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IResultHandler _resultHandler;
        private readonly ILogger _logger;

        protected ResourceClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger logger)
        {
            _connection = connection;
            _httpClientFactory = httpClientFactory;
            _resultHandler = resultHandler;
            _logger = logger;
        }

        private string Encode(string connectionLogin, string connectionPassword)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes($"{connectionLogin}:{connectionPassword}");
            return System.Convert.ToBase64String(bytes);
        }

        protected virtual async Task<IList<TEntity>> GetAsync<TEntity>(Query? query, string? customResourceIdentifier = null, CancellationToken cancellationToken = default)
        {
            var uri = GetUri(query, customResourceIdentifier: customResourceIdentifier);
            var client = GetClient();

            _logger.LogDebug($"HttpRequest: GET {uri}");
            
            var result = await client.GetAsync(uri, cancellationToken);
            _logger.LogDebug($"HttpResult: {result.StatusCode}");
            result.EnsureSuccessStatusCode();

            var json = await result.Content.ReadAsStringAsync();

            JObject jo = JObject.Parse(json);
            JArray list = (JArray)jo.SelectToken($"winstrom.{ResultIdentifier ?? ResourceIdentifier}");

            return list.ToObject<List<TEntity>>();
        }
        
        protected virtual async Task<TEntity> GetCustomAsync<TEntity>(string customResourceIdentifier, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage result = await GetCustomInternal(customResourceIdentifier, cancellationToken);
            return await result.Content.ReadAsAsync<TEntity>(cancellationToken);
        }
        
        protected virtual async Task<string> GetCustomAsync(string customResourceIdentifier, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage result = await GetCustomInternal(customResourceIdentifier, cancellationToken);
            return await result.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> GetCustomInternal(string customResourceIdentifier, CancellationToken cancellationToken)
        {
            var uri = GetUri(null, customResourceIdentifier: customResourceIdentifier);
            var client = GetClient();

            _logger.LogDebug($"HttpRequest: GET {uri}");
            
            var result = await client.GetAsync(uri, cancellationToken);
            _logger.LogDebug($"HttpResult: {result.StatusCode}");
            result.EnsureSuccessStatusCode();
            return result;
        }
        

        protected virtual Task<OperationResult<OperationResultDetail>> PostAsync<TRequest>(TRequest document, FlexiQuery? query = default, string? customResourceIdentifier = null, string? customRequestIdentifier = null, CancellationToken cancellationToken = default)
        {
            return SendAsync<TRequest, OperationResultDetail>(document, HttpMethod.Post, query, customResourceIdentifier, customRequestIdentifier, cancellationToken);
        }
        
        protected virtual Task<OperationResult<TResult>> PostAsync<TRequest, TResult>(TRequest document, FlexiQuery? query = default, string? customResourceIdentifier = null, string? customRequestIdentifier = null, CancellationToken cancellationToken = default)
        {
            return SendAsync<TRequest, TResult>(document, HttpMethod.Post, query, customResourceIdentifier, customRequestIdentifier, cancellationToken);
        }

        protected Task<OperationResult<OperationResultDetail>> PutAsync<TRequest>(TRequest document, FlexiQuery? query = default, string? customResourceIdentifier = null, string? customRequestIdentifier = null, CancellationToken cancellationToken = default)
        {
            return SendAsync<TRequest, OperationResultDetail>(document, HttpMethod.Put, query, customResourceIdentifier, customRequestIdentifier, cancellationToken);
        }
        
        protected Task<OperationResult<TResult>> PutAsync<TRequest, TResult>(TRequest document, FlexiQuery? query = default, string? customResourceIdentifier = null, string? customRequestIdentifier = null, CancellationToken cancellationToken = default)
        {
            return SendAsync<TRequest, TResult>(document, HttpMethod.Put, query, customResourceIdentifier, customRequestIdentifier, cancellationToken);
        }

        protected virtual async Task<OperationResult<TResult>> SendAsync<TRequest, TResult>(TRequest document, HttpMethod method, FlexiQuery? query = default, string? customResourceIdentifier = null, string? customRequestIdentifier = null, CancellationToken cancellationToken = default)
        {
            var result = await SendInternal(document, method, query, customResourceIdentifier, customRequestIdentifier, cancellationToken); 
            var resultContent = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                _logger.LogError(resultContent);
            }
            
            try
            {
                if (resultContent != null)
                {
                    _logger.LogTrace(resultContent);
                    var resultData = JsonConvert.DeserializeObject<WinstromEnvelope<TResult>>(resultContent);
                    if (resultData == null)
                    {
                        throw new InvalidOperationException($"Unable to deserialize type {typeof(TResult)}");
                    }
                    
                    await _resultHandler.ApplyFiltersAsync<TResult>(resultData.Data);
                    return new OperationResult<TResult>(result.StatusCode, resultData.Data);
                }
                return new OperationResult<TResult>(result.StatusCode);
            }
            catch (Exception ex)
            {
                return new OperationResult<TResult>(result.StatusCode, ex.Message);
            }
        }
        
        
        private async Task<HttpResponseMessage> SendInternal<TRequest>(TRequest document, HttpMethod method, FlexiQuery? query = default, string? customResourceIdentifier = null, string? requestIdentifier = null, CancellationToken cancellationToken = default)
        {
            var uri = GetUri(document, query, customResourceIdentifier);
            var client = GetClient();

            _logger.LogDebug($"HttpRequest: {method} {uri}");

            string jsonRequest;
            
            requestIdentifier = requestIdentifier ?? RequestIdentifier;
            
            if (customResourceIdentifier == null && requestIdentifier != null)
            {
                jsonRequest = JsonConvert.SerializeObject(new
                {
                    winstrom = new Dictionary<string, object>()
                    {
                        { requestIdentifier, document }
                    }
                });
            }
            else
            {
                jsonRequest = JsonConvert.SerializeObject(new
                {
                    winstrom = document
                });
            } 
            _logger.LogTrace(jsonRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(method, uri)
            {
                Content = content,
            };

            var result = await client.SendAsync(request, cancellationToken);
            _logger.LogDebug($"HttpResult: {result.StatusCode}");
            return result;
        }



        protected virtual string GetUri(Query? query, string? customResourceIdentifier = null)
        {
            return $"{_connection.Server}/c/{_connection.Company}/{customResourceIdentifier ?? ResourceIdentifier}{(query != null ? query.ToString() : string.Empty)}";
        }

        protected virtual string GetUri<TEnt>(TEnt document = default, FlexiQuery? query = default, string? customResourceIdentifier = null)
        {
            var uri = $"{_connection.Server}/c/{_connection.Company}/{customResourceIdentifier ?? ResourceIdentifier}";

            if (query != null)
            {
                uri += $"/query";
                
                if(query.Parameters.Any())
                    uri += $"?{FormatQuery(query)}";
            }
                

            return uri;
        }

        private string FormatQuery(FlexiQuery query)
        {
            return string.Join("&", query.Parameters.Select(s => $"{s.Key}={s.Value}"));
        }

        protected abstract string ResourceIdentifier { get; }
        protected virtual string? ResultIdentifier { get; }
        protected virtual string? RequestIdentifier => ResourceIdentifier;

        protected HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient(this.GetType().Name);
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Encode(_connection.Login, _connection.Password));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            return client;
        }
    }
}