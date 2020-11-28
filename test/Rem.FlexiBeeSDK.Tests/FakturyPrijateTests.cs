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
    public class FakturyPrijateTests
    {
        private FlexiBeeConnection _connection;

        public FakturyPrijateTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<FakturyPrijateTests>()
                .Build();

            _connection = configuration.GetSection("FlexiBeeConnection").Get<FlexiBeeConnection>();
            if(_connection == null)
             throw new ApplicationException($"{nameof(FlexiBeeConnection)} settings missing. Add configuration to user secrets");
        }


        [Fact]
        public async Task FindFakturyPrijate()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new FakturaPrijataClient(_connection, httpClient);

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = "kod='PF00492'",
                Relations = new List<Relations>()
                {
                    Relations.PolozkyDokladu
                }
            };

            var faktury = await client.FindAsync(query);

            Assert.NotEmpty(faktury);
            Assert.NotEmpty(faktury.First().PolozkyDokladu);
        }

        [Fact]
        public async Task GetFakturyPrijate()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new FakturaPrijataClient(_connection, httpClient);

            var faktura = await client.GetAsync("PF00492");

            Assert.NotNull(faktura);
            Assert.NotEmpty(faktura.PolozkyDokladu);
        }

        [Fact]
        public async Task GetFakturyPrijateNotFoundThrows()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new FakturaPrijataClient(_connection, httpClient);

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
