using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ERP.Domain.Repositories;
using ERP.Infrastructure.Data;
using ERP.Infrastructure.Services;
using ERP.Shared.Abstractions;

namespace ERP.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddScoped<IDateTimeService, DateTimeService>()
            .AddScoped<ITokenClaimsService, JwtClaimService>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection
        // https://github.com/khellang/Scrutor
        services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses(impl => impl.AssignableTo<IRepository>())
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
