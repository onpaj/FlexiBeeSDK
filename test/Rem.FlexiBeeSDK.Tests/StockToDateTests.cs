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
    public class StockToDateTests
    {
        private IFixture _fixture;

        public StockToDateTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetStockToDate()
        {
            var client = _fixture.Create<StockToDateClient>();

            var stockToDate = await client.GetAsync(DateTime.Now.Date, 5);

            Assert.NotEmpty(stockToDate);
        }
        
        [Fact]
        public async Task GetStockToDateData()
        {
            int limit = 10;
            var client = _fixture.Create<StockToDateClient>();

            var stockToDate = await client.GetAsync(DateTime.Now.Date, 5, limit: limit);

            Assert.NotEmpty(stockToDate);
            Assert.Equal(limit, stockToDate.Count);

            var first = stockToDate.First();
            Assert.NotEqual(0, first.ProductTypeId);
            Assert.False(string.IsNullOrEmpty(first.ProductCode));
            
            // Validate Supplier properties are loaded
            Assert.NotNull(first.SupplierCode);
            Assert.NotEqual(0, first.SupplierId);
            Assert.NotNull(first.SupplierName);
        }
    }
}
