using AdminApp.BlazorWasm.Helpers;
using AdminApp.BlazorWasm.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminApp.BlazorWasm.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private List<string> _notifications = new List<string>() { "Hello World..." };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var settings = Configuration.Get<AppSettings>();
                if (user.Identity.IsAuthenticated)
                {
                    var echoHubConnection = new HubConnectionBuilder()
                            .WithUrl(settings.EchoHubUrl, options =>
                            {
                                options.AccessTokenProvider = async () =>
                                {
                                    var accessTokenResult = await AccessTokenProvider.RequestAccessToken();
                                    accessTokenResult.TryGetToken(out var accessToken);
                                    return accessToken.Value;
                                };
                            })
                            .Build();
                    await echoHubConnection.StartAsync();
                    echoHubConnection.On<string>("Notification", OnNotificationReceived);
                    echoHubConnection.On<string>("Connected", OnNotificationReceived);
                }
                var broascastHubConnection = new HubConnectionBuilder()
                        .WithUrl(settings.BradcastHubUrl)
                        .Build();
                await broascastHubConnection.StartAsync();
                broascastHubConnection.On<string>("Notification", OnNotificationReceived);
                broascastHubConnection.On<string>("Connected", OnNotificationReceived);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private void OnNotificationReceived(string message)
        {
            var notification = JsonSerializer.Deserialize<PushNotification>(message);
            Console.WriteLine($"{notification.Description} at: {notification.IssuedAtUtc}");
            _notifications.Add($"{notification.Description} at: {notification.IssuedAtUtc}");
            StateHasChanged();
        }
    }
}
