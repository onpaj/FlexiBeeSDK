using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class FlexiBeeConnectionTests
    {
        private FlexiBeeConnection _connection;

        public FlexiBeeConnectionTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<FlexiBeeConnectionTests>()
                .Build();

            _connection = configuration.GetSection("FlexiBeeConnection").Get<FlexiBeeConnection>();
            if(_connection == null)
             throw new ApplicationException($"{nameof(FlexiBeeConnection)} settings missing. Add configuration to user secrets");
        }


        [Fact]
        public async Task GetKusovnik()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new KusovnikClient(_connection, httpClient);

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = "otecCenik='code:SER001030'",
            };

            var kusovnik = await client.GetAsync(query);

            Assert.NotEmpty(kusovnik);
        }

        [Fact]
        public async Task GetFakturyPrijate()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new KusovnikClient(_connection, httpClient);

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = "otecCenik='code:SER001030'",
            };

            var kusovnik = await client.GetAsync(query);

            Assert.NotEmpty(kusovnik);
        }
    }
}
