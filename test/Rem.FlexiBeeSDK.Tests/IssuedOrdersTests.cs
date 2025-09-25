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
    public class IssuedOrdersTests
    {
        private IFixture _fixture;

        public IssuedOrdersTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        
        [Fact]
        public async Task GetIssuedOrdersById()
        {
            var client = _fixture.Create<IssuedOrdersClient>();
            var id = 5226;
            var orders = await client.GetAsync(id);

            orders.Should().NotBeNull();
            orders.Id.Should().Be(id);
            orders.Items.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task GetIssued()
        {
            var client = _fixture.Create<IssuedOrdersClient>();

            var orders = await client.GetAsync(DateTime.Parse("2025-09-01"), DateTime.Parse("2025-09-10"));

           orders.Should().NotBeNull();
           orders.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task GetIssuedOrderByCode()
        {
            var code = "VYR50948";
            var client = _fixture.Create<IssuedOrdersClient>();

            var order = await client.GetByCodeAsync(code);
    
            order.Should().NotBeNull();
            order.Code.Should().Be(code);
        }
        
        [Fact]
        public async Task CreateIssuedOrder()
        {
            var client = _fixture.Create<IssuedOrdersClient>();

            var order = new CreateIssuedOrderFlexiDto()
            {
                OrderInternalNumber = "SER001001M - Bezstarostná krása - TEST",
                DateCreated = DateTime.Now,
                DateVat = DateTime.Now,
                DepartmentCode = "C",
                CreatedBy = "Heblo",
                Description = "Create by heblo - test!!",
                DocumentType = "VYR-POLOTOVAR",
                WarehouseDocumentType = "VYROBA-POLOTOVAR",
                Items = new List<IssuedOrderItemFlexiDto>()
                {
                    new IssuedOrderItemFlexiDto()
                    {
                        ProductCode = "SER001001M",
                        Name = "Bezstarostná krása - meziprodukt",
                        WarehouseCode = "POLOTOVARY",
                        Amount = 2,
                        LotNumber = "xxxx",
                        ExpirationDate = DateTime.Parse("2029-09-01"),
                    }
                },
            };
            
            var result = await client.SaveAsync(order);
    
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }
        
        [Fact]
        public async Task FinalizeIssuedOrder()
        {
            var client = _fixture.Create<IssuedOrdersClient>();
            var orderCode = "VYR51007";
            var order = await client.GetByCodeAsync(orderCode);
            
            var finalizeOrder = new FinalizeIssuedOrderFlexiDto(orderCode)
            {
                FinalizeStockMovement = new IssuedOrderStockMovementFlexiDto()
                {
                    WarehouseCode = "POLOTOVARY",
                    WarehouseDocumentType = "VYROBA-POLOTOVAR",
                    Items = new List<FinalizeIssuedOrderItemFlexiDto>()
                    {
                        new ()
                        {
                            Id = order.Items[0].Id,
                            Amount = order.Items[0].Amount,
                            LotNumber = "xxxx",
                            ExpirationDate = DateTime.Parse("2029-09-01"),
                        }
                    },
                }
            };
            
            var result = await client.FinalizeAsync(finalizeOrder);
    
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }
    }
}
