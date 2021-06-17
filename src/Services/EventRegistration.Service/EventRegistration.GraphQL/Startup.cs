using EventRegistration.Application;
using EventRegistration.GraphQL.Configs;
using EventRegistration.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventRegistration.GraphQL
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
            _dbConnectionName = "EventRegistrationDataConnection";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddIdentityServerAuth(_configuration);
            services.ConfigureHealthChecks(_configuration, _dbConnectionName);
            services.BuildGraphQLSchema();
            services.AddErrorFilter<ErrorFilter>();
            services.AddApplicationDependencies(_configuration);
            services.AddInfrastructureDependencies(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(opt =>
            {
                opt.MapGraphQL();
                opt.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
