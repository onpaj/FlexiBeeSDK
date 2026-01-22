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
    public class StockMovementTests
    {
        private IFixture _fixture;

        public StockMovementTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetStockMovementById()
        {
            var client = _fixture.Create<StockMovementClient>();

            // First get a movement by code to get its ID
            var movement = await client.GetByCodeAsync("S-21268/2025");
            movement.Should().NotBeNull("test data should exist");

            var id = movement.Id;

            // Now test GetAsync by ID
            var movementById = await client.GetAsync(id);

            movementById.Should().NotBeNull();
            movementById.Id.Should().Be(id);
            movementById.Code.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetStockMovementByCode()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            movement.Should().NotBeNull();
            movement.Code.Should().Be(code);
            movement.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetStockMovementsByDateRange()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-20");
            var dateTo = DateTime.Parse("2026-01-23");

            var movements = await client.GetAsync(dateFrom, dateTo, limit: 10);

            movements.Should().NotBeEmpty();
            movements.Should().OnlyContain(m => m.IssueDate >= dateFrom && m.IssueDate <= dateTo);
        }

        [Fact]
        public async Task GetStockMovementsByDirection_In()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-20");
            var dateTo = DateTime.Parse("2026-01-23");

            var movements = await client.GetAsync(
                dateFrom,
                dateTo,
                direction: StockMovementDirection.In,
                limit: 10);

            movements.Should().NotBeEmpty();
            movements.Should().OnlyContain(m => m.MovementTypeRaw == "typPohybu.prijem");
        }

        [Fact]
        public async Task GetStockMovementsByDirection_Out()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-20");
            var dateTo = DateTime.Parse("2026-01-23");

            var movements = await client.GetAsync(
                dateFrom,
                dateTo,
                direction: StockMovementDirection.Out,
                limit: 10);

            movements.Should().NotBeEmpty();
            movements.Should().OnlyContain(m => m.MovementTypeRaw == "typPohybu.vydej");
        }

        [Fact]
        public async Task GetStockMovementsByWarehouse()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-20");
            var dateTo = DateTime.Parse("2026-01-23");
            var warehouseCode = "MATERIAL";

            var movements = await client.GetAsync(
                dateFrom,
                dateTo,
                warehouseCode: warehouseCode,
                limit: 10);

            movements.Should().NotBeEmpty();
            movements.Should().OnlyContain(m =>
                m.WarehouseList != null &&
                m.WarehouseList.Count > 0 &&
                m.WarehouseList[0].Code == warehouseCode);
        }

        [Fact]
        public async Task GetStockMovementsByDocumentType()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-20");
            var dateTo = DateTime.Parse("2026-01-23");
            var documentTypeId = 45; // INVENTURA

            var movements = await client.GetAsync(
                dateFrom,
                dateTo,
                documentTypeId: documentTypeId,
                limit: 10);

            movements.Should().NotBeEmpty();
            movements.Should().OnlyContain(m =>
                m.DocumentTypeList != null &&
                m.DocumentTypeList.Count > 0 &&
                m.DocumentTypeList[0].Id == documentTypeId);
        }

        [Fact]
        public async Task CreateStockMovement()
        {
            var client = _fixture.Create<StockMovementClient>();

            var newMovement = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Test stock movement - automatický test",
                DocumentTypeCode = "code:INVENTURA",
                WarehouseCode = "code:MATERIAL",
                CompanyName = "Test vytvoření skladového pohybu",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:PMA002001M",
                        Quantity = 1,
                        PricePerUnit = 10.5m,
                        Name = "Testovací položka",
                    }
                }
            };

            var result = await client.SaveAsync(newMovement);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task UpdateStockMovement()
        {
            var client = _fixture.Create<StockMovementClient>();

            // First get an existing movement
            var existingMovement = await client.GetByCodeAsync("S-21268/2025");
            existingMovement.Should().NotBeNull("test data should exist");

            var updateMovement = new CreateStockMovementFlexiDto
            {
                Id = existingMovement.Id,
                IssueDate = existingMovement.IssueDate,
                Direction = existingMovement.MovementTypeRaw.Contains("prijem")
                    ? StockMovementDirection.In
                    : StockMovementDirection.Out,
                StockMovementTypeRaw = existingMovement.StockMovementTypeRaw,
                Description = $"{existingMovement.Description} - Updated by test",
                DocumentTypeCode = $"code:{existingMovement.DocumentTypeList[0].Code}",
                WarehouseCode = $"code:{existingMovement.WarehouseList[0].Code}",
                Note = "Updated via automated test"
            };

            var result = await client.UpdateAsync(updateMovement);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetStockMovement_WithAllFilters()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-20");
            var dateTo = DateTime.Parse("2026-01-23");

            var movements = await client.GetAsync(
                dateFrom,
                dateTo,
                direction: StockMovementDirection.In,
                warehouseCode: "MATERIAL",
                documentTypeId: 45,
                limit: 5);

            movements.Should().NotBeNull();
            // May be empty if no movements match all filters, which is acceptable
            foreach (var movement in movements)
            {
                movement.IssueDate.Should().BeOnOrAfter(dateFrom);
                movement.IssueDate.Should().BeOnOrBefore(dateTo);
                movement.MovementTypeRaw.Should().Be("typPohybu.prijem");
            }
        }

        [Fact]
        public async Task GetStockMovement_WithPagination()
        {
            var client = _fixture.Create<StockMovementClient>();
            var dateFrom = DateTime.Parse("2026-01-01");
            var dateTo = DateTime.Parse("2026-01-31");

            // First page
            var firstPage = await client.GetAsync(dateFrom, dateTo, limit: 5, skip: 0);
            firstPage.Should().NotBeNull();

            // Second page
            var secondPage = await client.GetAsync(dateFrom, dateTo, limit: 5, skip: 5);
            secondPage.Should().NotBeNull();

            // Pages should be different (if there are enough records)
            if (firstPage.Count >= 5 && secondPage.Count > 0)
            {
                firstPage.Should().NotIntersectWith(secondPage);
            }
        }
    }
}
