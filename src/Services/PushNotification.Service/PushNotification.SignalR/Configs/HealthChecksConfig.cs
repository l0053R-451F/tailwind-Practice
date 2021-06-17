using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PushNotification.SignalR.Configs
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration, string dbConnectionName)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                //.AddSqlServer(configuration.GetConnectionString(dbConnectionName),
                //name: "EventDB-check",
                //tags: new string[] { "EventDB" })
                .AddRabbitMQ(
                configuration["AppSettings:RabbitMQ:Uri"],
                name: "PushNotificationService-rabbitmqbus-check",
                tags: new string[] { "rabbitmqbus" });

            return services;
        }
    }
}
