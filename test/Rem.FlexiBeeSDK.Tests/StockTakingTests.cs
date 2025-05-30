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
using Rem.FlexiBeeSDK.Client.Clients.Products.BoM;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.StockTaking;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class StockTakingTests
    {
        private IFixture _fixture;

        private const int HeaderId = 43;

        public StockTakingTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        
        [Fact]
        public async Task GetHeader()
        {
            var client = _fixture.Create<StockTakingClient>();
            
            var header = await client.GetHeaderAsync(HeaderId);

            header.Should().NotBeNull();
            header.Id.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public async Task GetItems()
        {
            var client = _fixture.Create<StockTakingItemsClient>();
            
            var items = await client.GetStockTakingsAsync(HeaderId);

            items.Should().NotBeNull();
            items.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task AddItems()
        {
            var client = _fixture.Create<StockTakingItemsClient>();
            
            await client.AddStockTakingsAsync(HeaderId, 5, new []
            {
                new AddStockTakingItemRequest
                {
                    ProductCode = "AKL021",
                    Amount = 2000,
                    Lot = "101024",
                    Expiration = DateTime.Parse("2027-08-31"),
                }
            });
        }
        
        [Fact]
        public async Task AddMissingItems()
        {
            var client = _fixture.Create<StockTakingClient>();

            await client.AddMissingLotsAsync(HeaderId, [12919]);
        }
        
        [Fact]
        public async Task Recompute()
        {
            var client = _fixture.Create<StockTakingClient>();

            await client.RecomputeAsync(HeaderId);
        }
        
        [Fact]
        public async Task Submit()
        {
            var client = _fixture.Create<StockTakingClient>();

            await client.SubmitAsync(HeaderId, 60);
        }
        
        [Fact]
        public async Task CreateHeader()
        {
            var client = _fixture.Create<StockTakingClient>();

            var request = new StockTakingHeaderRequest
            {
                Date = DateTime.Now,
                WarehouseId = 5,
                Type = "Material - Heblo",
                Description = "Test Stock Taking",
                Owner = "Heblo",
                Executer = "Heblo"
            };
            var header = await client.CreateHeaderAsync(request);

            header.Should().NotBeNull();
            header.Id.Should().BeGreaterThan(0);
        }
    }
}
