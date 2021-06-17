using EventBus.Events;

namespace Profile.Application.IntegrationEvents
{
    public class NewUserSignedUpIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
