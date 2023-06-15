using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class SkladovePohybyTests
    {
        private IFixture _fixture;

        public SkladovePohybyTests()
        {
            _fixture = FlexiFixture.Setup();
        }


        [Fact]
        public async Task FindSkladovePohyby()
        {
            var client = _fixture.Create<SkladovyPohybClient>();

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
            var client = _fixture.Create<SkladovyPohybClient>();

            var skladovyPohyb = await client.GetAsync("S+00440/2020");

            Assert.NotNull(skladovyPohyb);
            Assert.NotEmpty(skladovyPohyb.PolozkyDokladu);
        }

        [Fact]
        public async Task GetSkladovePohybyNotFoundThrows()
        {
            var client = _fixture.Create<SkladovyPohybClient>();

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
