using EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PushNotification.Domain.Entities;
using PushNotification.SignalR.Hubs;
using PushNotification.SignalR.IntigrationEvents;
using System;
using System.Threading.Tasks;

namespace PushNotification.SignalR.IntigrationEventHandlers
{
    public class PushNotificationIntigrationEventHandler : IIntegrationEventHandler<PushNotificationIntigrationEvent>
    {
        private readonly ILogger<PushNotificationIntigrationEventHandler> _logger;
        private readonly IHubContext<BroadcastHub> _broadcastHubContext;
        private readonly IHubContext<EchoHub> _echoHubContext;

        public PushNotificationIntigrationEventHandler(ILogger<PushNotificationIntigrationEventHandler> logger, IHubContext<BroadcastHub> broadcastHubContext, IHubContext<EchoHub> echoHubContext)
        {
            _logger = logger;
            _broadcastHubContext = broadcastHubContext;
            _echoHubContext = echoHubContext;
        }

        public async Task HandleAsync(PushNotificationIntigrationEvent @event)
        {
            _logger.LogInformation("Handling Push Notification Event....");

            try
            {
                var notification = new Notification
                {
                    AudienceUserId = @event.AudienceUserId,
                    DeliveredAtUtc = DateTimeOffset.UtcNow,
                    Description = @event.Description,
                    Title = @event.Title,
                    Url = @event.Url,
                };
                //await _notificationService.PersistNotificationAsync(notification);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            var notificationObject = new
            {
                @event.Title,
                @event.Description,
                @event.Url,
                IssuedAtUtc = @event.CreateAtUtc
            };
            var notificationJson = JsonConvert.SerializeObject(notificationObject);
            if (!string.IsNullOrEmpty(@event.AudienceUserId?.Trim()))
            {
                await _echoHubContext.Clients.Group(@event.AudienceUserId)
                    .SendAsync("Notification", notificationJson);
            }
            else
            {
                await _broadcastHubContext.Clients.All
                    .SendAsync("Notification", notificationJson);
            }

            _logger.LogInformation("Handled Push Notification Event....");
        }
    }
}
