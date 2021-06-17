using AdminApp.BlazorWasm.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminApp.BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("IdentityServer", options.ProviderOptions);
            });
            AddServices(builder);
            await builder.Build().RunAsync();
        }

        private static void AddServices(WebAssemblyHostBuilder builder)
        {
            var settings = builder.Configuration.Get<AppSettings>();
            
            var eventService = settings.Services.FirstOrDefault(x => x.Name == "EventService");
            builder.Services.AddEventServiceClient();
            builder.Services.AddHttpClient("EventServiceClient", c => c.BaseAddress = new Uri($"{eventService.BaseUri}/graphql"));

            //var eventRegistrationService = settings.Services.FirstOrDefault(x => x.Name == "EventRegistrationService");
            //builder.Services.AddEventServiceClient();
            //builder.Services.AddHttpClient("EventServiceClient", c => c.BaseAddress = new Uri($"{eventRegistrationService.BaseUri}/graphql"));
        }
    }
}
