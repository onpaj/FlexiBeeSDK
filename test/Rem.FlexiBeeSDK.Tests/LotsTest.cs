using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Rem.FlexiBeeSDK.Client.Clients.Products.BoM;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Model;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class LotsTest
    {
        private IFixture _fixture;

        public LotsTest()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetSarze()
        {
            var client = _fixture.Create<LotsClient>();

            var sarze = await client.GetAsync();

            Assert.NotEmpty(sarze);
            var last = sarze.Last();

            Assert.NotNull(last.ProductCode);
        }
        
        [Fact]
        public async Task GetSarze_Take10()
        {
            var client = _fixture.Create<LotsClient>();

            var sarze = await client.GetAsync(limit: 10);

            Assert.NotEmpty(sarze);
            Assert.Equal(10, sarze.Count);
        }
        
        [Fact]
        public async Task GetSarze_NoLimit()
        {
            var client = _fixture.Create<LotsClient>();

            var sarze = await client.GetAsync(limit: 0);

            Assert.NotEmpty(sarze);
            Assert.True(sarze.Count > 20);
        }
        
        [Fact]
        public async Task GetSarze_SingleProduct()
        {
            var client = _fixture.Create<LotsClient>();

            var sarze = await client.GetAsync(productCode: "OLE024");

            Assert.NotEmpty(sarze);
            Assert.All(sarze.Select(s => s.ProductCode), s => Assert.Equal("OLE024", s));
        }
        
        [Fact]
        public async Task GetSarze_LotAndExpiration()
        {
            var client = _fixture.Create<LotsClient>();

            var sarze = await client.GetAsync(productCode: "OLE024");

            Assert.NotEmpty(sarze);
            var last = sarze.Last();
            Assert.Equal("OLE024", last.ProductCode);
            Assert.NotNull(last.Lot);
            Assert.NotNull(last.Expiration);
        }
    }
}
