using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PushNotification.Application.Interfaces;
using PushNotification.Infrastructure.DbContexts;
using PushNotification.Infrastructure.Services;

namespace PushNotification.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PushNotificationDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PushNotificationDataConnection"),
                    b => b.MigrationsAssembly(typeof(PushNotificationDataContext).Assembly.FullName)));

            services.AddScoped<IPushNotificationDataContext>(provider => provider.GetService<PushNotificationDataContext>());
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}
