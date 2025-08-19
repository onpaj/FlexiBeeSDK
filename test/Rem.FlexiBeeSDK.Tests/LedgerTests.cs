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
            string[] accountPrefixes = ["331"];
            

            var ledger = await client.GetAsync(dateFrom, dateTo, debitAccountPrefixes: accountPrefixes);

            ledger.Should().NotBeEmpty();
            ledger.Should().OnlyContain(w => w.DebitAccount != null && w.DebitAccount.Code.StartsWith(accountPrefixes.First()));
        }
        
        [Fact]
        public async Task GetLedgerByCreditAccount()
        {
            var client = _fixture.Create<LedgerClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-30");
            string[] accountPrefixes = ["331"];
            

            var ledger = await client.GetAsync(dateFrom, dateTo, creditAccountPrefixes: accountPrefixes);

            ledger.Should().NotBeEmpty();
            ledger.Should().OnlyContain(w => w.CreditAccount != null && w.CreditAccount.Code.StartsWith(accountPrefixes.First()));
        }
        
        [Fact]
        public async Task GetLedgerByMultipleAccounts()
        {
            var client = _fixture.Create<LedgerClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-30");
            string[] accountPrefixes = ["52","51"];
            

            var ledger = await client.GetAsync(dateFrom, dateTo, debitAccountPrefixes: accountPrefixes);

            ledger.Should().NotBeEmpty();
            ledger.Should().OnlyContain(w => w.CreditAccount != null && (w.DebitAccount.Code.StartsWith(accountPrefixes.First()) || w.DebitAccount.Code.StartsWith(accountPrefixes.Last())));
        }
        
        [Fact]
        public async Task GetLedgerByDepartmentId()
        {
            var client = _fixture.Create<LedgerClient>();
            var dateFrom = DateTime.Parse("2025-06-01");
            var dateTo = DateTime.Parse("2025-06-30");
            var departmentId = "VYROBA";
            

            var ledger = await client.GetAsync(dateFrom, dateTo, departmentId: departmentId);

            ledger.Should().NotBeEmpty();
            ledger.Should().OnlyContain(w => w.CreditAccount != null && w.Department.Code.Equals(departmentId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
