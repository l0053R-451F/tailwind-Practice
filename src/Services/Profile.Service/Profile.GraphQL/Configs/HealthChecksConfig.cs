using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Profile.GraphQL.Configs
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration, string dbConnectionName)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(configuration.GetConnectionString(dbConnectionName),
                name: "ProfileDB-check",
                tags: new string[] { "ProfileDB" })
                .AddRabbitMQ(
                configuration["AppSettings:RabbitMQ:Uri"],
                name: "ProfileService-rabbitmqbus-check",
                tags: new string[] { "rabbitmqbus" });

            return services;
        }
    }
}
