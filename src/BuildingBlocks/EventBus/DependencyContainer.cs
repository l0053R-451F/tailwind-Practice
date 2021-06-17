using EventBus.Abstractions;
using EventBus.RabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace EventBus
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var provider = sp.GetRequiredService<IServiceProvider>();
                var uri = new Uri(configuration["AppSettings:RabbitMQ:Uri"]);

                return new EventBusRabbitMQ(logger, uri, provider);
            });

            return services;
        }
    }
}
