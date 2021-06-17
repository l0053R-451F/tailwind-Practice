using EventBus.Events;

namespace Auth.IdentityServer.Oidc.Web.IntegrationEvents
{
    public class NewUserSignedUpIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
