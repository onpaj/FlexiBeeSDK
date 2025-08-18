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
using Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;
using Rem.FlexiBeeSDK.Client.Clients.Products.BoM;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Model;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class LedgerTests
    {
        private IFixture _fixture;

        public LedgerTests()
        {
            _fixture = FlexiFixture.Setup();
        }

        [Fact]
        public async Task GetLedgerByDate()
        {
            var client = _fixture.Create<LedgerClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-30");
            

            var ledger = await client.GetAsync(dateFrom, dateTo, limit: 10);

            ledger.Should().NotBeEmpty();

            ledger.Should().OnlyContain(w => w.AccountingDate>=dateFrom && w.AccountingDate <= dateTo);
        }
        
        [Fact]
        public async Task GetLedgerByDebitAccount()
        {
            var client = _fixture.Create<LedgerClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-30");
            var accountPrefix = "331";
            

            var ledger = await client.GetAsync(dateFrom, dateTo, debitAccountPrefix: accountPrefix);

            ledger.Should().NotBeEmpty();
            ledger.Should().OnlyContain(w => w.DebitAccount != null && w.DebitAccount.Code.StartsWith(accountPrefix));
        }
        
        [Fact]
        public async Task GetLedgerByCreditAccount()
        {
            var client = _fixture.Create<LedgerClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-30");
            var accountPrefix = "331";
            

            var ledger = await client.GetAsync(dateFrom, dateTo, creditAccountPrefix: accountPrefix);

            ledger.Should().NotBeEmpty();
            ledger.Should().OnlyContain(w => w.CreditAccount != null && w.CreditAccount.Code.StartsWith(accountPrefix));
        }
    }
}
