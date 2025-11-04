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
using Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices;
using Rem.FlexiBeeSDK.Model.Invoices;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class ReceivedInvoiceTests
    {
        private IFixture _fixture;

        public ReceivedInvoiceTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetFakturyPrijate()
        {
            var client = _fixture.Create<ReceivedInvoiceClient>();

            var faktura = await client.GetAsync("PF250951");

            Assert.NotNull(faktura);
            Assert.NotEmpty(faktura.Items);
        }

        [Fact]
        public async Task GetFakturyPrijateNotFoundThrows()
        {
            var client = _fixture.Create<ReceivedInvoiceClient>();

            await Assert.ThrowsAnyAsync<Exception>(() => client.GetAsync("xxxxx"));
        }
        
        [Fact]
        public async Task Search_ShouldReturnByLabel()
        {
            var client = _fixture.Create<ReceivedInvoiceClient>();
            var dateFrom = DateTime.Parse("2025-10-04");
            var dateTo = DateTime.Parse("2025-11-04");
            string label = "KLASIFIKACE";

            var invoices = await client.SearchAsync(new ReceivedInvoiceRequest(dateFrom, dateTo, label));

            invoices.Should().NotBeEmpty();
            invoices.Should().OnlyContain(d => d.Labels == label);
            var first = invoices.FirstOrDefault();
            
            first.Should().NotBeNull();
            first.Items.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task GetTags_ShouldReturnById()
        {
            var client = _fixture.Create<ReceivedInvoiceClient>();

            var tags = await client.GetTagsAsync(54327);
            tags.Should().NotBeNull();
            tags.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task AddTags_ShouldAddNewTag()
        {
            var invoiceId = 75642;
            var client = _fixture.Create<ReceivedInvoiceClient>();

            var tags = await client.GetTagsAsync(invoiceId);
            tags.Should().NotBeNull();
            tags.Should().NotContain(s => !string.IsNullOrEmpty(s));

            await client.AddTagAsync(invoiceId, "KLASIFIKACE");
            tags = await client.GetTagsAsync(invoiceId);
            tags.Should().NotBeNull();
            tags.Should().Contain(s => !string.IsNullOrEmpty(s));
            tags.Should().Contain("KLASIFIKACE");
            
            await client.RemoveTagAsync(invoiceId, "KLASIFIKACE");
            tags = await client.GetTagsAsync(invoiceId);
            tags.Should().NotBeNull();
            tags.Should().NotContain(s => !string.IsNullOrEmpty(s));
        }
    }
}
