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

            var faktura = await client.GetAsync("PF00492");

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
            var dateFrom = DateTime.Parse("2025-01-01");
            var dateTo = DateTime.Parse("2025-12-30");
            string label = "KLASIFIKACE";

            var departments = await client.SearchAsync(new ReceivedInvoiceRequest(dateFrom, dateTo, label));

            departments.Should().NotBeEmpty();
            departments.Should().OnlyContain(d => d.Labels == label);
        }
    }
}
