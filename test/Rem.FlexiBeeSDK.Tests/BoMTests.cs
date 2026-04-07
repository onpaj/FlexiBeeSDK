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
        public async Task UpdateIngredientAmount_ShouldSucceed()
        {
            var client = _fixture.Create<BoMClient>();

            // Fetch current BoM to get a real ingredient and its current amount
            var bom = await client.GetAsync("KRE003001M");
            var ingredient = bom.FirstOrDefault(i => i.Level != 1);
            ingredient.Should().NotBeNull("KRE003001M must have at least one non-header BoM item");

            var ingredientCode = ingredient!.IngredientCode;
            if (ingredientCode.StartsWith("code:"))
                ingredientCode = ingredientCode.Substring(5).Trim();

            // Update with the same amount — no net change, safe to run repeatedly
            var act = async () => await client.UpdateIngredientAmountAsync(
                "KRE003001M",
                ingredientCode,
                ingredient.Amount);

            await act.Should().NotThrowAsync();
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
    }
}
