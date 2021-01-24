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
    public class KusovnikTests
    {
        private IFixture _fixture;

        public KusovnikTests()
        {
            _fixture = FlexiFixture.Setup();
        }


        [Fact]
        public async Task FindKusovnik()
        {
            var client = _fixture.Create<KusovnikClient>();

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
            var client = _fixture.Create<KusovnikClient>();

            var kusovnik = await client.GetAsync("SER001030");

            Assert.NotEmpty(kusovnik);
        }



        [Fact]
        public async Task GetKusovnikNotFoundThrows()
        {
            var client = _fixture.Create<KusovnikClient>();

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
