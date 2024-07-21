using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices;
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
    }
}
