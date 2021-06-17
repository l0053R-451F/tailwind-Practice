using EventBus;
using EventBus.Abstractions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PushNotification.Application;
using PushNotification.Infrastructure;
using PushNotification.SignalR.Configs;
using PushNotification.SignalR.Hubs;
using PushNotification.SignalR.IntigrationEventHandlers;
using PushNotification.SignalR.IntigrationEvents;

namespace PushNotification.SignalR
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
            _dbConnectionName = "NotificationDataConnection";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureHealthChecks(_configuration, _dbConnectionName);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = _env.IsDevelopment();
            });
            services.AddIdentityServerAuth(_configuration);
            services.AddEventBusRabbitMQ(_configuration);
            services.AddApplicationDependencies(_configuration);
            services.AddInfrastructureDependencies(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHub<BroadcastHub>("/broadcast");
                endpoints.MapHub<EchoHub>("/echo");
            });

            var eventBusService = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBusService.SubscribeAsync<PushNotificationIntigrationEvent, PushNotificationIntigrationEventHandler>();
        }
    }
}
