using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ERP.Infrastructure.Data.Context;
using ERP.Shared.AppSettings;
using ERP.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api.Extensions;

[ExcludeFromCodeCoverage]
internal static class DbContextExtensions
{
    private const int DbMaxRetryCount = 3;
    private const int DbCommandTimeout = 35;
    private const string DbInMemoryName = $"Db-InMemory-{nameof(ErpContext)}";
    private const string DbMigrationAssemblyName = "ERP.Api";

    private static readonly string[] DbRelationalTags = { "database", "ef-core", "sql-server", "relational" };

    internal static IServiceCollection AddErpContext(this IServiceCollection services, IHealthChecksBuilder healthChecksBuilder)
    {
        services.AddDbContext<ErpContext>((serviceProvider, optionsBuilder) =>
        {
            var inMemoryOptions = serviceProvider.GetOptions<InMemoryOptions>();
            var strings = serviceProvider.GetOptions<ConnectionStrings>();

            if (inMemoryOptions.Database)
            {
                optionsBuilder.UseInMemoryDatabase(DbInMemoryName + $"-{Guid.NewGuid()}");
            }
            else
            {
                var connections = serviceProvider.GetOptions<ConnectionStrings>();

                optionsBuilder.UseNpgsql(connections.Database, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly(DbMigrationAssemblyName);
                    sqlServerOptions.EnableRetryOnFailure(DbMaxRetryCount);
                    sqlServerOptions.CommandTimeout(DbCommandTimeout);
                });
            }

            var logger = serviceProvider.GetRequiredService<ILogger<ErpContext>>();

            // Log tentativas de repetição
            optionsBuilder.LogTo(
                (eventId, _) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
                eventData =>
                {
                    if (eventData is not ExecutionStrategyEventData retryEventData)
                        return;

                    var exceptions = retryEventData.ExceptionsEncountered;

                    logger.LogWarning(
                        "----- Retry #{Count} with delay {Delay} due to error: {Message}",
                        exceptions.Count,
                        retryEventData.Delay,
                        exceptions[^1].Message);
                });

            var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
            if (!environment.IsProduction())
            {
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
            }
        });

        // Verificador de saúde da base de dados.
        healthChecksBuilder.AddDbContextCheck<ErpContext>(
            tags: DbRelationalTags,
            customTestQuery: (context, cancellationToken) =>
                context.Users.AsNoTracking().AnyAsync(cancellationToken));
        return services;
    }
}
