using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class SkladovePohybyTests
    {
        private FlexiBeeConnection _connection;

        public SkladovePohybyTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<SkladovePohybyTests>()
                .Build();

            _connection = configuration.GetSection("FlexiBeeConnection").Get<FlexiBeeConnection>();
            if(_connection == null)
             throw new ApplicationException($"{nameof(FlexiBeeConnection)} settings missing. Add configuration to user secrets");
        }


        [Fact]
        public async Task FindSkladovePohyby()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new SkladovyPohybClient(_connection, httpClient);

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = $"kod='{HttpUtility.UrlEncode("S+00440/2020'")}",
                Relations = new List<Relations>()
                {
                    Relations.PolozkyDokladu
                }
            };

            var pohyby = await client.FindAsync(query);

            Assert.NotEmpty(pohyby);
            Assert.NotEmpty(pohyby.First().PolozkyDokladu);
        }

        [Fact]
        public async Task GetSkladovePohyby()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new SkladovyPohybClient(_connection, httpClient);

            var skladovyPohyb = await client.GetAsync("S+00440/2020");

            Assert.NotNull(skladovyPohyb);
            Assert.NotEmpty(skladovyPohyb.PolozkyDokladu);
        }

        [Fact]
        public async Task GetSkladovePohybyNotFoundThrows()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new SkladovyPohybClient(_connection, httpClient);

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
