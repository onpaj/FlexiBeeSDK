using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Rem.FlexiBeeSDK.Client.Clients.IssuedOrders;
using Rem.FlexiBeeSDK.Client.Clients.Products.BoM;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.IssuedOrders;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class PriceListTests
    {
        private IFixture _fixture;

        public PriceListTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task UpdatePriceListWeight()
        {
            var client = _fixture.Create<PriceListClient>();

            var update = new PriceListFlexiDto()
            {
                ProductCode = "BEZ011",
                Weight = 999,
            };
            
            var result = await client.SaveAsync(update);
    
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }
    }
}
