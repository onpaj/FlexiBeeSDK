using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class ProductSetsTests
    {
        private IFixture _fixture;

        public ProductSetsTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetProductSet_BAL0025()
        {
            var client = _fixture.Create<ProductSetsClient>();
            var productCode = "BAL0025";

            var productSet = await client.GetAsync(productCode);

            productSet.Should().NotBeNull();
            productSet.Count.Should().Be(3);
        }
    }
}