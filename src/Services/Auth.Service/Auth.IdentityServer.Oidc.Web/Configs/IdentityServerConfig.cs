using Auth.IdentityServer.Oidc.Web.Data;
using Auth.IdentityServer.Oidc.Web.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.IdentityServer.Oidc.Web.Configs
{
    public static class IdentityServerConfig
    {
        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration, string dbConnectionName)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(dbConnectionName));
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryApiResources(InMemoryDataSet.GetApiResources())
                .AddInMemoryApiScopes(InMemoryDataSet.GetApiScopes())
                .AddInMemoryClients(InMemoryDataSet.GetClients(env))
                .AddInMemoryIdentityResources(InMemoryDataSet.GetIdentityResources())
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString(dbConnectionName),
                //        sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString(dbConnectionName),
                //        sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                //})
                .AddDeveloperSigningCredential();
            return services;
        }
    }
}
