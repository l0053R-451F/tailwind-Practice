using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profile.Application.Interfaces;
using Profile.Infrastructure.DbContexts;
using Profile.Infrastructure.Services;

namespace Profile.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddDbContext<ProfileDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProfileDataConnection"),
                    b => b.MigrationsAssembly(typeof(ProfileDataContext).Assembly.FullName)));
            services.AddScoped<IProfileDataContext>(provider => provider.GetService<ProfileDataContext>());
        }
    }
}
