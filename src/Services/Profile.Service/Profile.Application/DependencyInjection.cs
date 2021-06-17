using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using EventBus;
using EventBus.Abstractions;
using Profile.Application.Handlers.IntigrationEventHandlers;
using Profile.Application.IntegrationEvents;

namespace Profile.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddEventBusRabbitMQ(configuration);

            var eventBusService = services.BuildServiceProvider().GetRequiredService<IEventBus>();
            eventBusService.SubscribeAsync<NewUserSignedUpIntegrationEvent, NewUserSignedUpIntegrationEventHandler>();
        }
    }
}
