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

            var kusovnik = await client.GetAsync("KRE003001M");

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
        public async Task GetKusovnikNotFound()
        {
            var client = _fixture.Create<BoMClient>();

            var result = await client.GetAsync("xxxxx");
            
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task GetKusovnikByIngredient()
        {
            var client = _fixture.Create<BoMClient>();

            var kusovnik = await client.GetByIngredientAsync("AKL112");

            Assert.NotEmpty(kusovnik);
        }
        
        [Theory]
        [InlineData("OCH007250")]
        [InlineData("OCH007001M")]
        public async Task GetWeight(string productCode)
        {
            var client = _fixture.Create<BoMClient>();

            var weight = await client.GetBomWeight(productCode);

            weight.Should().NotBeNull();
            weight.ProductCode.Should().Be(productCode);
            weight.NetWeight.Should().BeGreaterThan(0);
            weight.Amount.Should().BeGreaterThan(0);
        }
        
        [Theory]
        [InlineData("AKL001")]
        public async Task GetWeightShouldBeNull(string productCode)
        {
            var client = _fixture.Create<BoMClient>();

            var weight = await client.GetBomWeight(productCode);

            weight.Should().BeNull();
        }

        [Fact]
        public async Task UpdateIngredientAmount_ChangesAndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            var ingredientCode = "KRE003001M";
            var productCode = "KRE003030";

            
            // Fetch current BoM to get a real ingredient
            var bom = await client.GetAsync(productCode);

            var ingredient = bom.FirstOrDefault(i => i.IngredientCode == ingredientCode);
            ingredient.Should().NotBeNull($"{ingredientCode} must have at least one non-header BoM item");

            var originalAmount = ingredient.Amount;
            var changedAmount = Math.Round(originalAmount + 0.101, 4);

            // Change the amount
            await client.UpdateIngredientAmountAsync(productCode, ingredientCode, changedAmount);

            // Verify the change persisted
            var updatedBom = await client.GetAsync(productCode);
            var updatedIngredient = updatedBom.FirstOrDefault(i =>
                i.Level != 1 && i.IngredientCode == ingredient.IngredientCode);
            updatedIngredient.Should().NotBeNull();
            updatedIngredient!.Amount.Should().BeApproximately(changedAmount, 0.0001);

            // Restore original amount
            await client.UpdateIngredientAmountAsync(productCode, ingredientCode, originalAmount);
        }

        [Fact]
        public async Task UpdateIngredientAmount_IngredientNotFound_ShouldThrow()
        {
            var client = _fixture.Create<BoMClient>();

            var act = async () => await client.UpdateIngredientAmountAsync(
                "KRE003001M",
                "DOES_NOT_EXIST",
                1.0);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*DOES_NOT_EXIST*");
        }

        [Fact]
        public async Task UpdateBoMItem_SetsNameC_AndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            const string productCode = "KRE003030";

            // Fetch current BoM and pick a non-header row
            var bom = await client.GetAsync(productCode);
            var row = bom.FirstOrDefault(r => r.Level != 1);
            row.Should().NotBeNull($"product {productCode} must have at least one non-header BoM row");

            var originalNameC = row!.NameC;
            var newNameC = $"sdk-test-{Guid.NewGuid():N}";

            // Set NameC
            await client.UpdateBoMItemAsync(row.Id, nameC: newNameC);

            // Verify persistence
            var updated = (await client.GetAsync(productCode)).Single(r => r.Id == row.Id);
            updated.NameC.Should().Be(newNameC);

            // Restore
            await client.UpdateBoMItemAsync(row.Id, nameC: originalNameC ?? string.Empty);
        }

        [Fact]
        public async Task UpdateBoMItem_SetsOrder_AndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            const string productCode = "KRE003030";

            var bom = await client.GetAsync(productCode);
            var row = bom.FirstOrDefault(r => r.Level != 1);
            row.Should().NotBeNull();

            var originalOrder = row!.Order;
            var newOrder = originalOrder + 100;

            await client.UpdateBoMItemAsync(row.Id, order: newOrder);

            var updated = (await client.GetAsync(productCode)).Single(r => r.Id == row.Id);
            updated.Order.Should().Be(newOrder);

            await client.UpdateBoMItemAsync(row.Id, order: originalOrder);
        }

        [Fact]
        public async Task UpdateBoMItem_ThrowsWhenNothingProvided()
        {
            var client = _fixture.Create<BoMClient>();

            var act = async () => await client.UpdateBoMItemAsync(id: 1);

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task SetItemsOrderAsync_BulkReorder_AndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            const string productCode = "KRE003030";

            var bom = await client.GetAsync(productCode);
            var rows = bom.Where(r => r.Level != 1).ToList();
            rows.Should().HaveCountGreaterThan(1,
                $"product {productCode} must have at least two non-header rows for a bulk reorder test");

            var originalPairs = rows.Select(r => (r.Id, r.Order)).ToList();

            // Swap order between the first two rows
            var swapped = new List<(int Id, int Order)>
            {
                (rows[0].Id, rows[1].Order),
                (rows[1].Id, rows[0].Order),
            };

            await client.SetItemsOrderAsync(swapped);

            // Verify
            var afterSwap = await client.GetAsync(productCode);
            afterSwap.Single(r => r.Id == rows[0].Id).Order.Should().Be(rows[1].Order);
            afterSwap.Single(r => r.Id == rows[1].Id).Order.Should().Be(rows[0].Order);

            // Restore
            await client.SetItemsOrderAsync(originalPairs);
        }

        [Fact]
        public async Task SetItemsOrderAsync_EmptyInput_IsNoOp()
        {
            var client = _fixture.Create<BoMClient>();

            var act = async () => await client.SetItemsOrderAsync(Array.Empty<(int, int)>());

            await act.Should().NotThrowAsync();
        }
    }
}
