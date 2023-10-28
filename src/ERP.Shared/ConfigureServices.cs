using System.Diagnostics.CodeAnalysis;
using ERP.Shared.Abstractions;
using ERP.Shared.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using ERP.Shared.Abstractions;
using ERP.Shared.AppSettings;
using Microsoft.Extensions.Configuration;

namespace ERP.Shared;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection ConfigureAppSettings(this IServiceCollection services) =>
        services
            .AddOptionsWithValidation<ConnectionStrings>()
            .AddOptionsWithValidation<InMemoryOptions>()
            .AddOptionsWithValidation<JwtOptions>();

    private static IServiceCollection AddOptionsWithValidation<TOptions>(this IServiceCollection services)
        where TOptions : class, IAppOptions
    {
        return services
            .AddOptions<TOptions>()
            .BindConfiguration(TOptions.ConfigSectionPath, binderOptions => binderOptions.BindNonPublicProperties = true)
            .ValidateDataAnnotations()
            .ValidateOnStart()
            .Services;
    }
}
