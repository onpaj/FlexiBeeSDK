using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class ObjednavkyVydaneTests
    {
        private IFixture _fixture;

        public ObjednavkyVydaneTests()
        {
            _fixture = FlexiFixture.Setup();
        }


        [Fact]
        public async Task FindObjednavkyVydane()
        {
            var client = _fixture.Create<ObjednavkaVydanaClient>();

            var query = new Query()
            {
                Format = Format.Json,
                LevelOfDetail = LevelOfDetail.Full,
                QueryString = "kod='VYR00405'",
                Relations = new List<Relations>()
                {
                    Relations.Items
                }
            };

            var faktury = await client.FindAsync(query);

            Assert.NotEmpty(faktury);
            Assert.NotEmpty(faktury.First().PolozkyDokladu);
        }

        [Fact]
        public async Task GetObjednavkyVydane()
        {
            var client = _fixture.Create<ObjednavkaVydanaClient>();

            var objednavka = await client.GetAsync("VYR00405");

            Assert.NotNull(objednavka);
            Assert.NotEmpty(objednavka.PolozkyDokladu);
        }

        [Fact]
        public async Task GetLinkedItems()
        {
            var client = _fixture.Create<ObjednavkaVydanaClient>();

            var linkedItems = await client.GetLinkedItems("VYR00406");

            Assert.NotEmpty(linkedItems);
        }

        [Fact]
        public async Task GetObjednavkyVydaneNotFoundThrows()
        {
            var client = _fixture.Create<ObjednavkaVydanaClient>();

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
