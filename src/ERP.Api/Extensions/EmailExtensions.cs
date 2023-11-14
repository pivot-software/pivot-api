namespace ERP.Api.Extensions
{
    public static class EmailExtensions
    {
        public static IServiceCollection AddSmtpSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpConfiguration>(configuration.GetSection("EmailConfiguration"));

            return services;
        }
    }
}
