using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.SignalR.Hubs
{
    [AllowAnonymous]
    public class BroadcastHub : Hub
    {
        private readonly ILogger<BroadcastHub> _logger;

        public BroadcastHub(ILogger<BroadcastHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId)
                .SendAsync($"Connected", $"Connected with Id: {Context.ConnectionId}.");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.Client(Context.ConnectionId)
                .SendAsync($"Disconnected", $"Id: {Context.ConnectionId} disconnected.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
