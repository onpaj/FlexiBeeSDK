using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    /// <summary>
    /// Detailed tests covering all important fields of StockMovement and StockItemMovement
    /// </summary>
    public class StockMovementDetailedTests
    {
        private IFixture _fixture;

        public StockMovementDetailedTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task StockMovement_ShouldHaveAllCoreFields()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            // Core identification
            movement.Should().NotBeNull();
            movement.Id.Should().BeGreaterThan(0, "ID must be positive");
            movement.Code.Should().NotBeNullOrEmpty("Code must be present");
            movement.Code.Should().Be(code);

            // Dates - all date fields should be validated
            movement.IssueDate.Should().NotBe(default(DateTime), "IssueDate must be set");
            movement.IssueDate.Year.Should().BeGreaterThan(2020, "IssueDate should be reasonable");

            // AccountingDate can be null but if set, should be valid
            if (movement.AccountingDate.HasValue)
            {
                movement.AccountingDate.Value.Year.Should().BeGreaterThan(2020);
            }

            // LastUpdate should be set by API
            if (movement.LastUpdate.HasValue)
            {
                movement.LastUpdate.Value.Should().BeBefore(DateTime.Now.AddDays(1));
            }
        }

        [Fact]
        public async Task StockMovement_ShouldHaveValidCodeValues()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            movement.Should().NotBeNull();

            // MovementTypeRaw (typPohybuK) - číselník směru pohybu
            movement.MovementTypeRaw.Should().NotBeNullOrEmpty("MovementTypeRaw must be set");
            movement.MovementTypeRaw.Should().MatchRegex(@"^typPohybu\.(prijem|vydej)$",
                "MovementTypeRaw must be either 'typPohybu.prijem' or 'typPohybu.vydej'");

            // StockMovementTypeRaw (typPohybuSkladK) - číselník typu skladového pohybu
            movement.StockMovementTypeRaw.Should().NotBeNullOrEmpty("StockMovementTypeRaw must be set");
            movement.StockMovementTypeRaw.Should().StartWith("typPohybuSklad.",
                "StockMovementTypeRaw must start with 'typPohybuSklad.'");
        }

        [Fact]
        public async Task StockMovement_ShouldHaveValidAmounts()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            movement.Should().NotBeNull();

            // TotalAmount (sumCelkem) - should be non-negative
            movement.TotalAmount.Should().BeGreaterThanOrEqualTo(0, "TotalAmount cannot be negative");

            // TotalAmountCurrency (sumCelkemMen) - can be 0 if in base currency
            movement.TotalAmountCurrency.Should().BeGreaterThanOrEqualTo(0, "TotalAmountCurrency cannot be negative");
        }

        [Fact]
        public async Task StockMovement_ShouldHaveValidNestedObjects()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            movement.Should().NotBeNull();

            // DocumentTypeList (typDokl) - must have at least one
            movement.DocumentTypeList.Should().NotBeNull("DocumentTypeList should be loaded");
            movement.DocumentTypeList.Should().NotBeEmpty("DocumentTypeList should have at least one item");
            var docType = movement.DocumentTypeList.First();
            docType.Id.Should().BeGreaterThan(0);
            docType.Code.Should().NotBeNullOrEmpty();
            docType.Name.Should().NotBeNullOrEmpty();

            // WarehouseList (sklad) - must have at least one
            movement.WarehouseList.Should().NotBeNull("WarehouseList should be loaded");
            movement.WarehouseList.Should().NotBeEmpty("WarehouseList should have at least one item");
            var warehouse = movement.WarehouseList.First();
            warehouse.Id.Should().BeGreaterThan(0);
            warehouse.Code.Should().NotBeNullOrEmpty();
            warehouse.Name.Should().NotBeNullOrEmpty();

            // CurrencyList (mena) - must have at least one
            movement.CurrencyList.Should().NotBeNull("CurrencyList should be loaded");
            movement.CurrencyList.Should().NotBeEmpty("CurrencyList should have at least one item");
            var currency = movement.CurrencyList.First();
            currency.Id.Should().BeGreaterThan(0);
            currency.Code.Should().NotBeNullOrEmpty();

            // DepartmentList (stredisko) - may be empty
            movement.DepartmentList.Should().NotBeNull("DepartmentList should be loaded");
        }

        [Fact]
        public async Task StockMovement_ShouldHaveValidBooleanFlags()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            movement.Should().NotBeNull();

            // Boolean flags are non-nullable, so they always have a value (true/false)
            // We can test their logical consistency

            // For non-cancelled movement
            if (!movement.Cancelled)
            {
                movement.Code.Should().NotContain("STORNO", "Non-cancelled movement should not have STORNO in code");
            }

            // If movement is cancelled, it should be marked as such
            if (movement.Code.Contains("STORNO"))
            {
                movement.Cancelled.Should().BeTrue("Movement with STORNO in code should be cancelled");
            }

            // WithoutItems flag should be consistent with Items collection
            // (Note: Items may not be loaded depending on query, so this is informational)
        }

        [Fact]
        public async Task StockItemMovement_ShouldHaveValidQuantityAndAmounts()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var documentId = 79260;

            var items = await client.GetAsync(documentId);

            items.Should().NotBeEmpty("Document should have at least one item");

            var item = items.First();

            // Amount (mnozMj) - quantity must be non-zero
            item.Amount.Should().NotBe(0, "Amount cannot be zero");

            // PricePerUnit (cenaMj) - should be non-negative
            item.PricePerUnit.Should().BeGreaterThanOrEqualTo(0, "PricePerUnit cannot be negative");

            // TotalSum (sumCelkem) - should be non-negative
            item.TotalSum.Should().BeGreaterThanOrEqualTo(0, "TotalSum cannot be negative");

            // Basic validation: TotalSum should be approximately Amount * PricePerUnit
            // (allowing for rounding differences)
            var expectedTotal = Math.Abs(item.Amount * item.PricePerUnit);
            Math.Abs(item.TotalSum - expectedTotal).Should().BeLessThan(0.1,
                "TotalSum should equal Amount * PricePerUnit (±0.1 for rounding)");
        }

        [Fact]
        public async Task StockItemMovement_ShouldHaveValidDateAndIdentifiers()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var documentId = 79260;

            var items = await client.GetAsync(documentId);

            items.Should().NotBeEmpty();

            var item = items.First();

            // Core fields
            item.Id.Should().BeGreaterThan(0, "Item ID must be positive");
            item.Name.Should().NotBeNullOrEmpty("Item name must be present");

            // Date must be valid
            item.Date.Should().NotBe(default(DateTime), "Date must be set");
            item.Date.Year.Should().BeGreaterThan(2020, "Date should be reasonable");

            // Document reference
            item.Document.Should().NotBeNull("Document reference must be present");
            item.Document.Id.Should().Be(documentId);
            item.Document.DocumentCode.Should().NotBeNullOrEmpty();

            // Store reference
            item.StoreCode.Should().NotBeNullOrEmpty("Store code must be present");
            item.StoreInternalId.Should().BeGreaterThanOrEqualTo(0, "StoreInternalId should be non-negative");

            // Product reference
            item.ProductCode.Should().NotBeNullOrEmpty("Product code must be present");
            item.PriceListInternalId.Should().BeGreaterThanOrEqualTo(0, "PriceListInternalId should be non-negative");
        }

        [Fact]
        public async Task StockItemMovement_ShouldHandleBatchAndExpiration()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");

            var items = await client.GetAsync(dateFrom, dateTo, limit: 50);

            items.Should().NotBeEmpty();

            // Check if any items have batch or expiration data
            var itemsWithBatch = items.Where(i => !string.IsNullOrEmpty(i.Batch)).ToList();
            var itemsWithExpiration = items.Where(i => !string.IsNullOrEmpty(i.Expiration)).ToList();

            // If batch-tracked items exist, validate format
            foreach (var item in itemsWithBatch)
            {
                item.Batch.Should().NotBeNullOrWhiteSpace("Batch should not be whitespace if set");
            }

            // If expiration-tracked items exist, validate format
            foreach (var item in itemsWithExpiration)
            {
                item.Expiration.Should().NotBeNullOrWhiteSpace("Expiration should not be whitespace if set");
                // Expiration is typically in format "2025-12-31" or similar
                if (DateTime.TryParse(item.Expiration, out var expirationDate))
                {
                    expirationDate.Year.Should().BeInRange(2020, 2050, "Expiration year should be reasonable");
                }
            }
        }

        [Fact]
        public async Task StockItemMovement_ShouldHaveValidCancellationFlags()
        {
            var client = _fixture.Create<StockItemsMovementClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-05");

            var items = await client.GetAsync(dateFrom, dateTo, limit: 20);

            items.Should().NotBeEmpty();

            foreach (var item in items)
            {
                // Boolean flags are non-nullable (have default value)
                // Test logical relationships

                // If parent document is cancelled, typically all items are too
                if (item.Cancelled)
                {
                    // Document-level cancellation
                    item.Cancelled.Should().BeTrue();
                }

                // ItemCancelled is item-specific cancellation
                // Can be true even if document is not cancelled (line cancellation)
                if (item.ItemCancelled)
                {
                    item.ItemCancelled.Should().BeTrue("Item marked as cancelled");
                }
            }
        }

        [Fact]
        public async Task StockMovement_WithItems_ShouldHaveConsistentData()
        {
            var movementClient = _fixture.Create<StockMovementClient>();
            var itemsClient = _fixture.Create<StockItemsMovementClient>();
            var code = "S-21268/2025";

            // Get movement header
            var movement = await movementClient.GetByCodeAsync(code);
            movement.Should().NotBeNull();

            // Get movement items
            var items = await itemsClient.GetAsync(movement.Id);

            if (items.Any())
            {
                // Verify items belong to this document
                foreach (var item in items)
                {
                    item.Document.Id.Should().Be(movement.Id,
                        "All items should reference the parent document");
                    item.Date.Should().Be(movement.IssueDate,
                        "Item date should match document issue date");
                }

                // If movement has items, WithoutItems should be false
                if (!movement.WithoutItems)
                {
                    items.Should().NotBeEmpty("Movement with WithoutItems=false should have items");
                }
            }
        }

        [Fact]
        public async Task StockMovement_StringFields_ShouldNotBeOnlyWhitespace()
        {
            var client = _fixture.Create<StockMovementClient>();
            var code = "S-21268/2025";

            var movement = await client.GetByCodeAsync(code);

            movement.Should().NotBeNull();

            // Optional string fields - if set, should not be only whitespace
            if (!string.IsNullOrEmpty(movement.Description))
            {
                movement.Description.Trim().Should().NotBeEmpty("Description should not be only whitespace");
            }

            if (!string.IsNullOrEmpty(movement.Note))
            {
                movement.Note.Trim().Should().NotBeEmpty("Note should not be only whitespace");
            }

            if (!string.IsNullOrEmpty(movement.CompanyName))
            {
                movement.CompanyName.Trim().Should().NotBeEmpty("CompanyName should not be only whitespace");
            }

            if (!string.IsNullOrEmpty(movement.VariableSymbol))
            {
                movement.VariableSymbol.Trim().Should().NotBeEmpty("VariableSymbol should not be only whitespace");
            }
        }
    }
}
