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
    public class ObjednavkyVydaneTests
    {
        private FlexiBeeConnection _connection;

        public ObjednavkyVydaneTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ObjednavkyVydaneTests>()
                .Build();

            _connection = configuration.GetSection("FlexiBeeConnection").Get<FlexiBeeConnection>();
            if(_connection == null)
             throw new ApplicationException($"{nameof(FlexiBeeConnection)} settings missing. Add configuration to user secrets");
        }


        [Fact]
        public async Task FindObjednavkyVydane()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new ObjednavkaVydanaClient(_connection, httpClient);

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = "kod='VYR00405'",
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
        public async Task GetObjednavkyVydane()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new ObjednavkaVydanaClient(_connection, httpClient);

            var objednavka = await client.GetAsync("VYR00405");

            Assert.NotNull(objednavka);
            Assert.NotEmpty(objednavka.PolozkyDokladu);
        }

        [Fact]
        public async Task GetLinkedItems()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new ObjednavkaVydanaClient(_connection, httpClient);

            var linkedItems = await client.GetLinkedItems("VYR00406");

            Assert.NotEmpty(linkedItems);
        }

        [Fact]
        public async Task GetObjednavkyVydaneNotFoundThrows()
        {
            var httpClient = HttpClientFactory.Create();
            var client = new ObjednavkaVydanaClient(_connection, httpClient);

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
