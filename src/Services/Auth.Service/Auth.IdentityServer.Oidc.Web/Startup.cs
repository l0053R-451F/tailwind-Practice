using Auth.IdentityServer.Oidc.Web.Configs;
using Auth.IdentityServer.Oidc.Web.Data;
using EventBus;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth.IdentityServer.Oidc.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly string _dbConnectionName;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            _dbConnectionName = "IdentityOidcDataConnection";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllersWithViews();
            services.ConfigureIdentityServer(_env, _configuration, _dbConnectionName);
            services.ConfigureHealthChecks(_configuration, _dbConnectionName);
            services.AddEventBusRabbitMQ(_configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            app.UseRouting();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseAuthorization();
            app.UseEndpoints(opt =>
            {
                opt.MapDefaultControllerRoute();
                opt.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
