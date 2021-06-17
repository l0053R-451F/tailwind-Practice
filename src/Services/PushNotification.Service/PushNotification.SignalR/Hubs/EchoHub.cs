using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.SignalR.Hubs
{
    [Authorize]
    public class EchoHub : Hub
    {
        private readonly ILogger<EchoHub> _logger;

        public EchoHub(ILogger<EchoHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var userId = Context.User?.Claims?.First(x => x.Type == JwtClaimTypes.Subject).Value;
                _logger.LogInformation($"Authorized user connected with id: {userId}");
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                await Clients.Client(Context.ConnectionId)
                .SendAsync($"Connected", $"Connected with Id: {Context.ConnectionId}.");
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.Client(Context.ConnectionId)
                .SendAsync($"Disconnected", $"Id: {Context.ConnectionId} disconnected.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
