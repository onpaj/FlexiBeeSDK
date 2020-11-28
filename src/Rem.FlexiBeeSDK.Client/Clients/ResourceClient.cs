using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public abstract class ResourceClient<TEntity> : IResourceClient<TEntity>
    {
        private readonly FlexiBeeConnection _connection;
        private readonly HttpClient _httpClient;

        protected ResourceClient(FlexiBeeConnection connection, HttpClient httpClient)
        {
            _connection = connection;
            _httpClient = httpClient;
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Encode(connection.Login, connection.Password) );
        }

        private string Encode(string connectionLogin, string connectionPassword)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes($"{connectionLogin}:{connectionPassword}");
            return System.Convert.ToBase64String(bytes);
        }

        public virtual async Task<IList<TEntity>> FindAsync(Query query, CancellationToken cancellationToken = default)
        {
            var uri = GetUri(query);

            var result = await _httpClient.GetAsync(uri, cancellationToken);
            result.EnsureSuccessStatusCode();

            var json = await result.Content.ReadAsStringAsync();

            JObject jo = JObject.Parse(json);
            JArray list = (JArray)jo.SelectToken($"winstrom.{ResourceIdentifier}");

            return list.ToObject<List<TEntity>>();
        }

        protected Uri GetUri(Query query)
        {
            return new Uri($"{_connection.Server}/c/{_connection.Company}/{ResourceIdentifier}/{query}");
        }

        protected abstract string ResourceIdentifier { get; }
    }
}