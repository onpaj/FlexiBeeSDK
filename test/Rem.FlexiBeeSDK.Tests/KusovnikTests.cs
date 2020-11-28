using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class KusovnikTests
    {
        private FlexiBeeConnection _connection;

        public KusovnikTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<FakturyPrijateTests>()
                .Build();

            _connection = configuration.GetSection("FlexiBeeConnection").Get<FlexiBeeConnection>();
            if(_connection == null)
             throw new ApplicationException($"{nameof(FlexiBeeConnection)} settings missing. Add configuration to user secrets");
        }


        [Fact]
        public async Task FindKusovnik()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new KusovnikClient(_connection, httpClient);

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = "otecCenik='code:SER001030'",
            };

            var kusovnik = await client.FindAsync(query);

            Assert.NotEmpty(kusovnik);
        }

        [Fact]
        public async Task GetKusovnik()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new KusovnikClient(_connection, httpClient);

            var kusovnik = await client.GetAsync("SER001030");

            Assert.NotEmpty(kusovnik);
        }



        [Fact]
        public async Task GetKusovnikNotFoundThrows()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new KusovnikClient(_connection, httpClient);

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
