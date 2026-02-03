using System;
using System.Collections.Generic;
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
    /// Comprehensive tests for creating stock movements (receipts and issues) with items, batches, and expiration dates
    /// </summary>
    public class StockMovementCreateTests
    {
        private IFixture _fixture;

        public StockMovementCreateTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task CreateStockMovement_Receipt_WithMultipleItems()
        {
            var client = _fixture.Create<StockMovementClient>();

            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Test příjemka - automatický test",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test vytvoření příjemky s položkami",
                Note = $"Vytvořeno automatickým testem {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 10.5m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Testovací položka se šarží",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"TEST-BATCH-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddYears(2).ToString("yyyy-MM-dd"),
                        Note = "Položka s šarží a expirací"
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:EOL009",
                        Quantity = 5.0m,
                        PricePerUnit = 15.75m,
                        Name = "EOL009 - Další položka se šarží",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"TEST-BATCH-EOL-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddMonths(18).ToString("yyyy-MM-dd"),
                        Note = "Druhá položka s šarží"
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:ETI007",
                        Quantity = 20.0m,
                        PricePerUnit = 8.25m,
                        Name = "ETI007 - Položka bez šarže a expirace",
                        WarehouseCode = "code:MATERIAL",
                        // ETI007 nemá šarži a expiraci - necháme null
                        Batch = null,
                        Expiration = null,
                        Note = "Položka bez batch trackovani"
                    }
                }
            };

            var result = await client.SaveAsync(receipt);

            // Validace výsledku
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();

            // Validace že máme ID vytvořeného dokladu
            if (result.IsSuccess && result.Result?.Results?.Any() == true)
            {
                var createdIdString = result.Result?.Results?.FirstOrDefault()?.Id;
                createdIdString.Should().NotBeNullOrEmpty("Created document should have an ID");

                // Pokus načíst vytvořený doklad pro verifikaci
                if (!string.IsNullOrEmpty(createdIdString) && int.TryParse(createdIdString, out var createdId))
                {
                    var created = await client.GetAsync(createdId);
                    created.Should().NotBeNull("Created document should be retrievable");
                    created.MovementTypeRaw.Should().Be("typPohybu.prijem");
                }
            }
        }

        [Fact]
        public async Task CreateStockMovement_Issue_WithMultipleItems()
        {
            var client = _fixture.Create<StockMovementClient>();

            var issue = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.Out,
                StockMovementTypeRaw = "typPohybuSklad.vydejHoly",
                Description = "Test výdejka - automatický test",
                DocumentTypeCode = "code:VYROBA-PRODUKT",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test vytvoření výdejky s položkami",
                Note = $"Vytvořeno automatickým testem {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 3.5m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Výdej",
                        WarehouseCode = "code:MATERIAL",
                        // Pro výdej nenecháváme systém automaticky určit šarži podle FIFO
                        Batch = null,
                        Expiration = null,
                        Note = "Výdej položky - šarže určena systémem"
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:EOL009",
                        Quantity = 2.0m,
                        PricePerUnit = 15.75m,
                        Name = "EOL009 - Výdej další položky",
                        WarehouseCode = "code:MATERIAL",
                        Batch = null,
                        Expiration = null,
                        Note = "Výdej druhé položky - šarže určena systémem"
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:ETI007",
                        Quantity = 8.0m,
                        PricePerUnit = 8.25m,
                        Name = "ETI007 - Výdej bez šarže",
                        WarehouseCode = "code:MATERIAL",
                        // ETI007 nemá šarži a expiraci
                        Batch = null,
                        Expiration = null,
                        Note = "Výdej bez batch trackování"
                    }
                }
            };

            var result = await client.SaveAsync(issue);

            // Validace výsledku
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();

            // Validace že máme ID vytvořeného dokladu
            if (result.IsSuccess && result.Result?.Results?.Any() == true)
            {
                var createdIdString = result.Result?.Results?.FirstOrDefault()?.Id;
                createdIdString.Should().NotBeNullOrEmpty("Created document should have an ID");

                // Pokus načíst vytvořený doklad pro verifikaci
                if (!string.IsNullOrEmpty(createdIdString) && int.TryParse(createdIdString, out var createdId))
                {
                    var created = await client.GetAsync(createdId);
                    created.Should().NotBeNull("Created document should be retrievable");
                    created.MovementTypeRaw.Should().Be("typPohybu.vydej");
                }
            }
        }

        [Fact]
        public async Task CreateStockMovement_Receipt_OnlyBatchTrackedItems()
        {
            var client = _fixture.Create<StockMovementClient>();

            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Příjem pouze položek se šarží",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test - pouze batch tracked items",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 15.0m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Batch tracked",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"BATCH-AKL-{DateTime.Now:yyyyMMddHHmmss}",
                        Expiration = DateTime.Now.AddYears(3).ToString("yyyy-MM-dd")
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:EOL009",
                        Quantity = 10.0m,
                        PricePerUnit = 15.75m,
                        Name = "EOL009 - Batch tracked",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"BATCH-EOL-{DateTime.Now:yyyyMMddHHmmss}",
                        Expiration = DateTime.Now.AddMonths(24).ToString("yyyy-MM-dd")
                    }
                }
            };

            var result = await client.SaveAsync(receipt);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateStockMovement_Issue_OnlyNonBatchTrackedItems()
        {
            var client = _fixture.Create<StockMovementClient>();

            var issue = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.Out,
                StockMovementTypeRaw = "typPohybuSklad.vydejHoly",
                Description = "Výdej pouze položek bez šarže",
                DocumentTypeCode = "code:VYROBA-PRODUKT",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test - pouze non-batch items",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:ETI007",
                        Quantity = 25.0m,
                        PricePerUnit = 8.25m,
                        Name = "ETI007 - Non-batch tracked",
                        WarehouseCode = "code:MATERIAL",
                        Batch = null,
                        Expiration = null
                    }
                }
            };

            var result = await client.SaveAsync(issue);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateStockMovement_Receipt_WithDifferentQuantities()
        {
            var client = _fixture.Create<StockMovementClient>();

            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Test různých množství",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test různých množství a cen",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 0.5m, // Malé množství
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Malé množství",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"SMALL-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddYears(2).ToString("yyyy-MM-dd")
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:EOL009",
                        Quantity = 100.75m, // Velké množství
                        PricePerUnit = 15.75m,
                        Name = "EOL009 - Velké množství",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"LARGE-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddMonths(18).ToString("yyyy-MM-dd")
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:ETI007",
                        Quantity = 1.0m, // Jednotkové množství
                        PricePerUnit = 8.25m,
                        Name = "ETI007 - Jednotkové množství",
                        WarehouseCode = "code:MATERIAL"
                    }
                }
            };

            var result = await client.SaveAsync(receipt);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateStockMovement_Receipt_WithShortExpiration()
        {
            var client = _fixture.Create<StockMovementClient>();

            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Test krátké expirace",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test krátké doby trvanlivosti",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 5.0m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Krátká expirace",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"SHORT-EXP-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"),
                        Note = "Položka s krátkou dobou trvanlivosti - 3 měsíce"
                    },
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:EOL009",
                        Quantity = 3.0m,
                        PricePerUnit = 15.75m,
                        Name = "EOL009 - Velmi krátká expirace",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"VERYSHORT-EXP-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                        Note = "Položka s velmi krátkou dobou trvanlivosti - 1 měsíc"
                    }
                }
            };

            var result = await client.SaveAsync(receipt);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateStockMovement_Receipt_WithLongExpiration()
        {
            var client = _fixture.Create<StockMovementClient>();

            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Test dlouhé expirace",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test dlouhé doby trvanlivosti",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 50.0m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Dlouhá expirace",
                        WarehouseCode = "code:MATERIAL",
                        Batch = $"LONG-EXP-{DateTime.Now:yyyyMMdd}",
                        Expiration = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd"),
                        Note = "Položka s dlouhou dobou trvanlivosti - 5 let"
                    }
                }
            };

            var result = await client.SaveAsync(receipt);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());
            result.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateStockMovement_Receipt_VerifyCreatedDocument()
        {
            var client = _fixture.Create<StockMovementClient>();
            var itemsClient = _fixture.Create<StockItemsMovementClient>();
            var testBatch = $"VERIFY-BATCH-{DateTime.Now:yyyyMMddHHmmss}";

            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Test verifikace vytvořeného dokladu",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Verifikační test",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 7.5m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Verifikační položka",
                        WarehouseCode = "code:MATERIAL",
                        Batch = testBatch,
                        Expiration = DateTime.Now.AddYears(2).ToString("yyyy-MM-dd")
                    }
                }
            };

            // Vytvoření dokladu
            var result = await client.SaveAsync(receipt);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue(result.GetErrorMessage());

            // Získání ID vytvořeného dokladu
            var createdIdString = result.Result?.Results?.FirstOrDefault()?.Id;
            createdIdString.Should().NotBeNullOrEmpty("Created document should have an ID");

            if (!string.IsNullOrEmpty(createdIdString) && int.TryParse(createdIdString, out var createdId))
            {
                // Načtení hlavičky
                var created = await client.GetAsync(createdId);
                created.Should().NotBeNull();
                created.MovementTypeRaw.Should().Be("typPohybu.prijem");
                created.Description.Should().Be("Test verifikace vytvořeného dokladu");

                // Načtení položek
                var items = await itemsClient.GetAsync(createdId);
                items.Should().NotBeEmpty();
                items.Should().HaveCount(1);

                var item = items.First();
                item.ProductCode.Should().Be("AKL011");
                item.Amount.Should().Be(7.5);
                item.Batch.Should().Be(testBatch);
            }
        }

        [Fact]
        public async Task CreateStockMovement_ReceiptThenIssue_SpecificBatch()
        {
            var client = _fixture.Create<StockMovementClient>();
            var itemsClient = _fixture.Create<StockItemsMovementClient>();
            var testBatch = $"BATCH-SPECIFIC-{DateTime.Now:yyyyMMddHHmmss}";
            var testExpiration = DateTime.Now.AddYears(2).ToString("yyyy-MM-dd");

            // Krok 1: Vytvoření příjemky s konkrétní šarží
            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Příjem pro test batch-specific výdeje",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test batch-specific operations",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 20.0m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Příjem pro test",
                        WarehouseCode = "code:MATERIAL",
                        Batch = testBatch,
                        Expiration = testExpiration
                    }
                }
            };

            var receiptResult = await client.SaveAsync(receipt);
            receiptResult.IsSuccess.Should().BeTrue(receiptResult.GetErrorMessage());

            // Krok 2: Výdej ze stejné konkrétní šarže
            var issue = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.Out,
                StockMovementTypeRaw = "typPohybuSklad.vydejHoly",
                Description = "Výdej konkrétní šarže",
                DocumentTypeCode = "code:VYROBA-PRODUKT",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test batch-specific issue",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 5.0m, // Vydáváme méně než jsme přijali
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Výdej konkrétní šarže",
                        WarehouseCode = "code:MATERIAL",
                        Batch = testBatch, // Specifikujeme STEJNOU šarži
                        Expiration = testExpiration
                    }
                }
            };

            var issueResult = await client.SaveAsync(issue);

            // Validace výdeje
            issueResult.Should().NotBeNull();
            issueResult.IsSuccess.Should().BeTrue(issueResult.GetErrorMessage());

            // Ověření že výdej byl vytvořen s konkrétní šarží
            var issueIdString = issueResult.Result?.Results?.FirstOrDefault()?.Id;
            if (!string.IsNullOrEmpty(issueIdString) && int.TryParse(issueIdString, out var issueId))
            {
                var issueItems = await itemsClient.GetAsync(issueId);
                issueItems.Should().NotBeEmpty();
                var issuedItem = issueItems.First();
                issuedItem.Batch.Should().Be(testBatch, "Issued item should have the specified batch");
                issuedItem.Expiration.Should().StartWith(testExpiration,
                    "Issued item should have the specified expiration (API may add timezone info)");
            }
        }

        [Fact]
        public async Task CreateStockMovement_Issue_NonExistentBatch_ShouldFail()
        {
            var client = _fixture.Create<StockMovementClient>();
            var nonExistentBatch = $"NONEXISTENT-BATCH-{DateTime.Now:yyyyMMddHHmmss}";

            var issue = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.Out,
                StockMovementTypeRaw = "typPohybuSklad.vydejHoly",
                Description = "Test výdeje neexistující šarže",
                DocumentTypeCode = "code:VYROBA-PRODUKT",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test - validace neexistující šarže",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 1.0m,
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Pokus o výdej neexistující šarže",
                        WarehouseCode = "code:MATERIAL",
                        Batch = nonExistentBatch,
                        Expiration = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd")
                    }
                }
            };

            var result = await client.SaveAsync(issue);

            // Očekáváme, že operace selže - šarže neexistuje na skladě
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse("Cannot issue from a batch that doesn't exist in stock");
            result.Result?.Results?.FirstOrDefault()?.Errors.Should().NotBeNullOrEmpty(
                "Error should indicate insufficient stock for the specified batch");
        }

        [Fact]
        public async Task CreateStockMovement_Issue_ExceedBatchQuantity_ShouldFail()
        {
            var client = _fixture.Create<StockMovementClient>();
            var testBatch = $"BATCH-EXCEED-{DateTime.Now:yyyyMMddHHmmss}";
            var testExpiration = DateTime.Now.AddYears(2).ToString("yyyy-MM-dd");

            // Krok 1: Příjem malého množství
            var receipt = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.In,
                StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
                Description = "Příjem malého množství pro test překročení",
                DocumentTypeCode = "code:VYROBA-POLOTOVAR",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test - příjem pro validaci",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 5.0m, // Přijímáme pouze 5 kusů
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Malý příjem",
                        WarehouseCode = "code:MATERIAL",
                        Batch = testBatch,
                        Expiration = testExpiration
                    }
                }
            };

            var receiptResult = await client.SaveAsync(receipt);
            receiptResult.IsSuccess.Should().BeTrue(receiptResult.GetErrorMessage());

            // Krok 2: Pokus o výdej většího množství než je na skladě
            var issue = new CreateStockMovementFlexiDto
            {
                IssueDate = DateTime.Now,
                Direction = StockMovementDirection.Out,
                StockMovementTypeRaw = "typPohybuSklad.vydejHoly",
                Description = "Pokus o výdej většího množství",
                DocumentTypeCode = "code:VYROBA-PRODUKT",
                WarehouseCode = "code:MATERIAL",
                DepartmentCode = "code:C",
                CompanyName = "Test - validace překročení množství",
                WithoutItems = false,
                Items = new List<CreateStockMovementItemFlexiDto>
                {
                    new CreateStockMovementItemFlexiDto
                    {
                        ProductCode = "code:AKL011",
                        Quantity = 10.0m, // Pokoušíme se vydat 10, ale máme jen 5
                        PricePerUnit = 25.50m,
                        Name = "AKL011 - Pokus o výdej nad limit",
                        WarehouseCode = "code:MATERIAL",
                        Batch = testBatch,
                        Expiration = testExpiration
                    }
                }
            };

            var issueResult = await client.SaveAsync(issue);

            // Očekáváme, že operace selže - nedostatek zboží v šarži
            issueResult.Should().NotBeNull();
            issueResult.IsSuccess.Should().BeFalse(
                "Cannot issue more quantity than available in the specified batch");
            issueResult.Result?.Results?.FirstOrDefault()?.Errors.Should().NotBeNullOrEmpty(
                "Error should indicate insufficient quantity in batch");
        }
    }
}
