using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Rem.FlexiBeeSDK.Client;
using Rem.FlexiBeeSDK.Client.Clients;
using Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices;
using Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices;
using Rem.FlexiBeeSDK.Model.Invoices;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class IssuedInvoiceTests
    {
        private IFixture _fixture;

        public IssuedInvoiceTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task ImportIssedInvoice()
        {
            var client = _fixture.Create<IssuedInvoiceClient>();

            var faktura = await client.SaveAsync(new IssuedInvoice());

            Assert.NotNull(faktura);
            Assert.NotEmpty(faktura.Items);
        }
    }
}
