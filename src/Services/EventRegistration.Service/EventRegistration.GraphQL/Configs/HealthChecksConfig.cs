using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EventRegistration.GraphQL.Configs
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration, string dbConnectionName)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(configuration.GetConnectionString(dbConnectionName),
                name: "EventRegistrationDB-check",
                tags: new string[] { "EventRegistrationDB" })
                .AddRabbitMQ(
                configuration["AppSettings:RabbitMQ:Uri"],
                name: "EventRegistration-rabbitmqbus-check",
                tags: new string[] { "rabbitmqbus" });

            return services;
        }
    }
}
