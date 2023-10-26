using ERP.Infrastructure.data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ERP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddContext(configuration);
        // services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }


    // public static IServiceCollection AddPersistence(
    //     this IServiceCollection services)
    // {
    //     services.AddScoped<IUserRepository, UserRepository>();
    //     services.AddScoped<IAdminstratorRepository, AdminstratorRepository>();
    //     services.AddScoped<IAccountsRepository, AccountsRepository>();
    //     services.AddScoped<ICustomerRepository, CustomersRepository>();
    //     services.AddScoped<ITransactionsRepository, TransactionsRepository>();
    //     services.AddScoped<IDispositionRepository, DispositionRepository>();
    //
    //     return services;
    // }

    private static IServiceCollection AddContext(
        this IServiceCollection services, ConfigurationManager configuration)
    {

        var pgsqlSettings = new PgsqlSettings();

        configuration.Bind(PgsqlSettings.SectionName, pgsqlSettings);
        services.AddSingleton(Options.Create(pgsqlSettings));
        services.AddSingleton<PgsqlContext>();

        return services;
    }


    //     public static IServiceCollection AddAuth(
    //     this IServiceCollection services, ConfigurationManager configuration)
    // {
    //     var JwtSettings = new JwtSettings();
    //     configuration.Bind(JwtSettings.SectionName, JwtSettings);
    //
    //     services.AddSingleton(Options.Create(JwtSettings));
    //     services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    //
    //     services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    //         .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
    //         {
    //             ValidateIssuer = true,
    //             ValidateAudience = true,
    //             ValidateLifetime = true,
    //             ValidateIssuerSigningKey = true,
    //             ValidIssuer = JwtSettings.Issuer,
    //             ValidAudience = JwtSettings.Audience,
    //             IssuerSigningKey = new SymmetricSecurityKey(
    //                 Encoding.UTF8.GetBytes(JwtSettings.Secret))
    //         });
    //     return services;
    // }
}
