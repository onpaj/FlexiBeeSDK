using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class FakturyPrijateTests
    {
        private IFixture _fixture;

        public FakturyPrijateTests()
        {
            _fixture = FlexiFixture.Setup();
        }


        [Fact]
        public async Task FindFakturyPrijate()
        {
            var client = _fixture.Create<FakturaPrijataClient>();

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
            var client = _fixture.Create<FakturaPrijataClient>();

            var faktura = await client.GetAsync("PF00492");

            Assert.NotNull(faktura);
            Assert.NotEmpty(faktura.PolozkyDokladu);
        }

        [Fact]
        public async Task GetFakturyPrijateNotFoundThrows()
        {
            var client = _fixture.Create<FakturaPrijataClient>();

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
