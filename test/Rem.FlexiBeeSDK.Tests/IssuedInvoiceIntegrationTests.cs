using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Rem.FlexiBeeSDK.Client.Clients.Banks;
using Rem.FlexiBeeSDK.Client.Clients.CashRegisters;
using Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices;
using Rem.FlexiBeeSDK.Model.Invoices;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class IssuedInvoiceIntegrationTests
{
    private readonly IFixture _fixture;
    private readonly IssuedInvoiceClient _client;

    public IssuedInvoiceIntegrationTests()
    {
        _fixture = FlexiFixture.Setup();

        // Inject real clients so unpair calls actually hit the API
        var bankClient = _fixture.Create<BankClient>();
        _fixture.Inject<IBankClient>(bankClient);
        var cashRegisterClient = _fixture.Create<CashRegisterClient>();
        _fixture.Inject<ICashRegisterClient>(cashRegisterClient);

        _client = _fixture.Create<IssuedInvoiceClient>();
    }

    [Fact]
    public async Task GetAllAsync_ForDateRange_ShouldReturn275InvoicesWithItemsAndPrices()
    {
        var dateFrom = new DateTime(2026, 4, 15);
        var dateTo = new DateTime(2026, 4, 15);

        var invoices = await _client.GetAllAsync(dateFrom, dateTo);

        invoices.Should().HaveCount(275);
        invoices.Should().AllSatisfy(invoice =>
        {
            invoice.Items.Should().NotBeNullOrEmpty($"invoice {invoice.Code} should have items");
            invoice.Items.Should().AllSatisfy(item =>
            {
                item.PricePerUnit.Should().NotBe(0, $"item {item.Code} on invoice {invoice.Code} should have a price");
            });
        });
    }

    [Fact]
    public async Task SaveAsync_WithUnpairIfNecessary_ShouldSucceed()
    {
        const string invoiceCode = "125029454";
        var today = DateTime.Today.ToString("yyyy-MM-dd");

        // Fetch existing invoice to obtain its internal Id (required for update, not create)
        var existing = await _client.GetAsync(invoiceCode);

        // Items from GET carry their internal Ids. With ItemsRemoveAll=true (default),
        // FlexiBee removes and re-adds them — so Ids must be cleared. Computed sum
        // fields are recalculated by FlexiBee and must not be sent.
        var items = existing.Items.Select(i => new IssuedInvoiceItemFlexiDto
        {
            Code = i.Code,
            Name = i.Name,
            Amount = i.Amount,
            PricePerUnit = i.PricePerUnit,
            VatRateType = i.VatRateType,
            MeasureUnit = i.MeasureUnit,
            PriceList = i.PriceList,
            Store = i.Store,
        }).ToList();

        var invoice = new IssuedInvoiceDetailFlexiDto
        {
            Id = existing.Id,
            Code = invoiceCode,
            DocumentType = "code:FAKTURA",
            VarSymbol = invoiceCode,
            Description = "Test invoice - integration test",
            DateCreated = today,
            DateDue = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd"),
            DateTaxOrig = today,
            DateTaxAcc = today,
            // Address loaded directly from existing invoice
            CompanyName = existing.CompanyName,
            CompanyStreet = existing.CompanyStreet,
            CompanyCity = existing.CompanyCity,
            CompanyState = existing.CompanyState,
            CIN = existing.CIN,
            VATIN = existing.VATIN,
            Items = items
        };

        var result = await _client.SaveAsync(invoice, unpairIfNecessary: true);

        result.IsSuccess.Should().BeTrue(result.GetErrorMessage());

        var saved = await _client.GetAsync(invoiceCode);
        saved.Code.Should().Be(invoiceCode);
        saved.Description.Should().Be("Test invoice - integration test");
    }
}
