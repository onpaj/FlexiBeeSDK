using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class StockItemsMovementsTests
    {
        private IFixture _fixture;

        public StockItemsMovementsTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetStockItemsByDate()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");
            

            var items = await client.GetAsync(dateFrom, dateTo, limit: 10);

            items.Should().NotBeEmpty();

            items.Should().OnlyContain(w => w.Date >=dateFrom && w.Date <= dateTo);
        }
        
        [Fact]
        public async Task GetStockItemsByStoreCode()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");
            var storeCode = "MATERIAL";
            

            var items = await client.GetAsync(dateFrom, dateTo, storeCode: storeCode, limit: 10);

            items.Should().NotBeEmpty();

            items.Should().OnlyContain(w => w.Date >=dateFrom && w.Date <= dateTo);
            items.Should().OnlyContain(w => w.StoreCode == storeCode);
        }
        
        [Fact]
        public async Task GetStockItemsByDocumentTypeId()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");
            var documentTypeId = 56;

            var items = await client.GetAsync(dateFrom, dateTo, documentTypeId: documentTypeId, limit: 10);

            items.Should().NotBeEmpty();

            items.Should().OnlyContain(w => w.Date >=dateFrom && w.Date <= dateTo);
            items.Should().OnlyContain(w => w.Document.DocumentTypeId == documentTypeId);
        }
        
        [Fact]
        public async Task GetStockItemsByDocumentCode()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");
            var documentCode = "S+00320/2025";

            var items = await client.GetAsync(dateFrom, dateTo, documentCode: documentCode, limit: 10);

            items.Should().NotBeEmpty();

            items.Should().OnlyContain(w => w.Date >=dateFrom && w.Date <= dateTo);
            items.Should().OnlyContain(w => w.Document.DocumentCode == documentCode);
        }
        
        [Fact]
        public async Task GetStockItemsByDocumentId()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var documentId = 79260;

            var items = await client.GetAsync(documentId);

            items.Should().NotBeEmpty();

            items.Should().OnlyContain(w => w.Document.Id == documentId);
        }
        
        [Fact]
        public async Task GetStockItemsByDirection()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");

            var items = await client.GetAsync(dateFrom, dateTo, direction: StockMovementDirection.Out, limit: 10);

            items.Should().NotBeEmpty();

            items.Should().OnlyContain(w => w.Date >=dateFrom && w.Date <= dateTo);
            items.Should().OnlyContain(w => w.Document.MovementDirectionCode == "typPohybu.vydej");
        }
        
        [Fact]
        public async Task CreateStockItemsMovement()
        {
            var client = _fixture.Create<StockItemsMovementClient>();

            var request = new StockItemsMovementUpsertRequestFlexiDto()
            {
                CreatedBy = "heblo",
                AccountingDate = DateTime.Now,
                IssueDate = DateTime.Now,
                StockItems = new List<StockItemsMovementUpsertRequestItemFlexiDto>()
                {
                    new()
                    {
                        ProductCode = "PMA002001M",
                        ProductName = "Moje Moringová - meziprodukt",
                        Amount = 2,
                        AmountIssued = 2,
                        LotNumber = null,
                        Expiration = null,
                        UnitPrice = 1.82,
                    }
                },
                Description = "MO-2025-027",
                DocumentTypeCode = "VYROBA-POLOTOVAR",
                StockMovementDirection = StockMovementDirection.Out,
                Note = "MO-2025-027 - Note",
            };
            var result = await client.SaveAsync(request);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }
    }
}
