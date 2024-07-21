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
using Rem.FlexiBeeSDK.Model;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class BoMTests
    {
        private IFixture _fixture;

        public BoMTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetKusovnik()
        {
            var client = _fixture.Create<BoMClient>();

            var kusovnik = await client.GetAsync("SER001030");

            Assert.NotEmpty(kusovnik);
        }

        [Fact]
        public async Task RecalculatePurchasePrice()
        {
            var client = _fixture.Create<BoMClient>();

            var result = await client.RecalculatePurchasePrice(3244);

            Assert.True(result);
        }

        [Fact]
        public async Task GetKusovnikNotFoundThrows()
        {
            var client = _fixture.Create<BoMClient>();

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
    }
}
