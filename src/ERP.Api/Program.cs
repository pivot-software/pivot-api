using System.IO.Compression;
using ERP.Api.Extensions;
using ERP.API.Extensions;
using ERP.Application;
using ERP.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ERP.Infrastructure.Data.Context;
using ERP.Shared;
using ERP.Shared.AppSettings;
using ERP.Shared.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// #region [Healthcheck]
//
// builder.Services.AddHealthChecks()
//     .AddNpgSql("Server=localhost;Database=ERP;User Id=postgres;Password=jl99oe99",
//         name: "postgreSQL", tags: new string[] { "db", "data" });
// // .AddRedis(builder.Configuration.GetSection("DatabaseSettings:ConnectionStringRedis").Value,
// //     name: "redis", tags: new string[] { "cache", "data" });
//
// builder.Services.AddHealthChecksUI(opt =>
// {
//     opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
//     opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
//     opt.SetApiMaxActiveRequests(1); //api requests concurrency
//
//     opt.AddHealthCheckEndpoint("default api", "/health"); //map health check api
// }).AddInMemoryStorage();
//
// #endregion


builder.Services
    .Configure<GzipCompressionProviderOptions>(
        compressionOptions => compressionOptions.Level = CompressionLevel.Fastest)
    .Configure<RouteOptions>(routeOptions => routeOptions.LowercaseUrls = true);

builder.Services.AddHttpClient()
    .AddHttpContextAccessor()
    .AddResponseCompression(compressionOptions =>
    {
        compressionOptions.EnableForHttps = true;
        compressionOptions.Providers.Add<GzipCompressionProvider>();
    })
    .ConfigureAppSettings()
    .AddJwtBearer(builder.Configuration, builder.Environment.IsProduction())
    .AddInfrastructure()
    .AddRepositories()
    .AddErpContext()
    .AddSmtpSender(builder.Configuration)
    .AddServices()
    .AddCors(options =>
    {
        options.AddPolicy("MyCorsPolicy",
            builder => builder.WithOrigins("http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader());
    })
    .AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });


builder.Services.AddDataProtection();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressMapClientErrors = true;
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Host.UseDefaultServiceProvider((context, serviceProviderOptions) =>
{
    serviceProviderOptions.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    serviceProviderOptions.ValidateOnBuild = true;
});

builder.WebHost.UseKestrel(kestrelOptions => kestrelOptions.AddServerHeader = false);

var app = builder.Build();

await using var serviceScope = app.Services.CreateAsyncScope();
await using var context = serviceScope.ServiceProvider.GetRequiredService<ErpContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    #region [Healthcheck]

    app.UseHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    }).UseHealthChecksUI(options =>
    {
        options.UIPath = "/health-ui";
        options.ApiPath = "/health-api";
        options.UseRelativeApiPath = false;
        options.UseRelativeResourcesPath = false;
    });

    #endregion
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("MyCorsPolicy");

var inMemoryOptions = serviceScope.ServiceProvider.GetOptions<InMemoryOptions>();

try
{
    app.Logger.LogInformation("----- AutoMapper: Validando os mapeamentos...");

    app.Logger.LogInformation("----- AutoMapper: Mapeamentos são válidos!");

    if (inMemoryOptions.Database)
    {
        app.Logger.LogInformation("----- Database InMemory: Criando e migrando a base de dados...");
        await context.Database.EnsureCreatedAsync();
    }
    else
    {
        var connectionString = context.Database.GetConnectionString();
        app.Logger.LogInformation("----- SQL Server: {Connection}", connectionString);
        app.Logger.LogInformation("----- SQL Server: Verificando se existem migrações pendentes...");

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            app.Logger.LogInformation("----- SQL Server: Criando e migrando a base de dados...");

            await context.Database.MigrateAsync();

            app.Logger.LogInformation("----- SQL Server: Base de dados criada e migrada com sucesso!");
        }
        else
        {
            app.Logger.LogInformation("----- SQL Server: Migrações estão em dia");
        }
    }
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Ocorreu uma exceção ao iniciar a aplicação: {Message}", ex.Message);
    throw;
}

app.Run();