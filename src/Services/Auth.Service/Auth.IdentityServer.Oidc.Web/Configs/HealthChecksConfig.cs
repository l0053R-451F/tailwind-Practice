using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Auth.IdentityServer.Oidc.Web.Configs
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration, string dbConnectionName)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(configuration.GetConnectionString(dbConnectionName),
                name: "IdentityDB-check",
                tags: new string[] { "IdentityDB" })
                .AddRabbitMQ(
                configuration["AppSettings:RabbitMQ:Uri"],
                name: "Identity-rabbitmqbus-check",
                tags: new string[] { "rabbitmqbus" });

            return services;
        }
    }
}
