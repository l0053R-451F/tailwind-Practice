using EventRegistration.Application.Interfaces;
using EventRegistration.Infrastructure.DbContexts;
using EventRegistration.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventRegistration.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddDbContext<EventRegistrationDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EventRegistrationDataConnection"),
                    b => b.MigrationsAssembly(typeof(EventRegistrationDataContext).Assembly.FullName)));
            services.AddScoped<IEventRegistrationDataContext>(provider => provider.GetService<EventRegistrationDataContext>());
        }
    }
}
