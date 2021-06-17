namespace Event.GraphQL.Payloads
{
    public class CreateEventPayload
    {
        public CreateEventPayload(Domain.Entities.Event @event, string clientMutationId)
        {
            Event = @event;
            ClientMutationId = clientMutationId;
        }

        public Domain.Entities.Event Event { get; }
        public string ClientMutationId { get; }
    }
}
