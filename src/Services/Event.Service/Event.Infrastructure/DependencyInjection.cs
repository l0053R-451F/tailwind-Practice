using Event.Application.Interfaces;
using Event.Infrastructure.DbContexts;
using Event.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddDbContext<EventDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EventDataConnection"),
                    b => b.MigrationsAssembly(typeof(EventDataContext).Assembly.FullName)));
            services.AddScoped<IEventDataContext>(provider => provider.GetService<EventDataContext>());
        }
    }
}
