using System.Linq;
using HotChocolate.Data;

namespace Event.GraphQL.Payloads
{
    public class UpdateEventPayload
    {
        public UpdateEventPayload(IQueryable<Domain.Entities.Event> @event, string clientMutationId)
        {
            Event = @event;
            ClientMutationId = clientMutationId;
        }

        [UseFirstOrDefault]
        [UseProjection]
        public IQueryable<Domain.Entities.Event> Event { get; private set; }
        public string ClientMutationId { get; private set; }
    }
}
