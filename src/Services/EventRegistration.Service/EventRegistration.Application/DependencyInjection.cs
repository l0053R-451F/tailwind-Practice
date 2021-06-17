using System.Reflection;
using EventBus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventRegistration.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddEventBusRabbitMQ(configuration);
        }
    }
}
