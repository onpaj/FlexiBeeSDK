using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rem.FlexiBeeSDK.Client.Clients;
using Rem.FlexiBeeSDK.Client.Clients.Accounting;
using Rem.FlexiBeeSDK.Client.Clients.Accounting.Departments;
using Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;
using Rem.FlexiBeeSDK.Client.Clients.BankAccounts;
using Rem.FlexiBeeSDK.Client.Clients.Banks;
using Rem.FlexiBeeSDK.Client.Clients.Contacts;
using Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices;
using Rem.FlexiBeeSDK.Client.Clients.IssuedOrders;
using Rem.FlexiBeeSDK.Client.Clients.Products.BoM;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;
using Rem.FlexiBeeSDK.Client.Clients.ReceivedInvoices;
using Rem.FlexiBeeSDK.Client.ResultFilters;

namespace Rem.FlexiBeeSDK.Client.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlexiBee(this IServiceCollection services, IConfiguration configuration)
    {
        var flexiSettings = configuration.GetSection(FlexiBeeSettings.ConfigNodeName);
        services.Configure<FlexiBeeSettings>(flexiSettings);
        services.AddSingleton(flexiSettings.Get<FlexiBeeSettings>());

        services.AddSingleton<IIssuedInvoiceClient, IssuedInvoiceClient>();
        services.AddSingleton<IBankClient, BankClient>();
        services.AddSingleton<IBoMClient, BoMClient>();
        services.AddSingleton<IBankAccountClient, BankAccountClient>();
        services.AddSingleton<IStockToDateClient, StockToDateClient>();
        services.AddSingleton<IStockItemsMovementClient, StockItemsMovementClient>();
        services.AddSingleton<IStockMovementClient, StockMovementClient>();
        services.AddSingleton<ILedgerClient, LedgerClient>();
        services.AddSingleton<IAccountingTemplateClient, AccountingTemplateClient>();
        services.AddSingleton<IDepartmentClient, DepartmentClient>();
        services.AddSingleton<IContactListClient, ContactListClient>();
        services.AddSingleton<IContactClient, ContactClient>();
        services.AddSingleton<IProductSetsClient, ProductSetsClient>();
        services.AddSingleton<IPriceListClient, PriceListClient>();
        services.AddSingleton<IReceivedInvoiceClient, ReceivedInvoiceClient>();
        services.AddSingleton<ILotsClient, LotsClient>();
        services.AddSingleton<IStockTakingClient, StockTakingClient>();
        services.AddSingleton<IStockTakingItemsClient, StockTakingItemsClient>();
        services.AddScoped<IIssuedOrdersClient, IssuedOrdersClient>();
        
        
        
        
        services.AddSingleton<IResultHandler, ResultHandler>();

        // Result filters
        services.AddSingleton<IResultFilter, AlreadyPairedResultFilter>();
        services.AddSingleton<IResultFilter, UnknownProductResultFilter>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return services;
    }
}