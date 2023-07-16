using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rem.FlexiBeeSDK.Client.Clients;
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
        services.AddSingleton<IResultHandler, ResultHandler>();

        // Result filters
        services.AddSingleton<IResultFilter, AlreadyPairedResultFilter>();
        services.AddSingleton<IResultFilter, UnknownProductResultFilter>();

        return services;
    }
}