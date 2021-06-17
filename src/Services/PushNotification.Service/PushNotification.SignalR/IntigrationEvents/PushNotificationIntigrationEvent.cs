using EventBus.Events;

namespace PushNotification.SignalR.IntigrationEvents
{
    public class PushNotificationIntigrationEvent : IntegrationEvent
    {
        public string AudienceUserId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Url { get; }

        public PushNotificationIntigrationEvent(string title, string description, string audienceUserId = null, string url = null)
        {
            Title = title;
            Description = description;
            AudienceUserId = audienceUserId;
            Url = url;
        }
    }
}
