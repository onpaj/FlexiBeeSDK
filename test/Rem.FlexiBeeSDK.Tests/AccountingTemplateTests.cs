using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Accounting;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class AccountingTemplateTests
    {
        private IFixture _fixture;

        public AccountingTemplateTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetAccountingTemplates_ShouldReturnTemplates()
        {
            var client = _fixture.Create<AccountingTemplateClient>();

            var templates = await client.GetAsync();

            templates.Should().NotBeEmpty();
            templates.Should().OnlyContain(t => !string.IsNullOrEmpty(t.Code));
            templates.Should().OnlyContain(t => !string.IsNullOrEmpty(t.Name));
            templates.Should().OnlyContain(t => t.Id > 0);
        }

        [Fact]
        public async Task GetAccountingTemplates_ShouldContainExpectedProperties()
        {
            var client = _fixture.Create<AccountingTemplateClient>();

            var templates = await client.GetAsync();

            templates.Should().NotBeEmpty();
            
            var firstTemplate = templates.First();
            firstTemplate.Id.Should().BeGreaterThan(0);
            firstTemplate.Code.Should().NotBeNullOrEmpty();
            firstTemplate.Name.Should().NotBeNullOrEmpty();
            firstTemplate.AccountCode.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task UpdateInvoice_ShouldReturnUpdatedTemplate()
        {
            var client = _fixture.Create<AccountingTemplateClient>();

            var templates = await client.UpdateInvoiceAsync("PF250051", "SLUŽBY-IT", "VYROBA");

            templates.IsSuccess.Should().BeTrue($"{templates.StatusCode}:{templates.ErrorMessage}");
            templates.Result?.Stats?.Updated.Should().Be("1");

            var result = templates.Result?.Results?.FirstOrDefault();
            result.Should().NotBeNull();
        }
    }
}