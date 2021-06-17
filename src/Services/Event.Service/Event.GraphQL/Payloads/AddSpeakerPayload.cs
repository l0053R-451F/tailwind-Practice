using System.Linq;
using Event.Domain.Entities;
using HotChocolate.Data;
using HotChocolate.Types;

namespace Event.GraphQL.Payloads
{
    public class AddSpeakerPayload
    {
        public AddSpeakerPayload(string clientMutationId, IQueryable<EventSpeaker> eventSpeaker)
        {
            ClientMutationId = clientMutationId;
            EventSpeaker = eventSpeaker;
        }

        public string ClientMutationId { get; }
        [UseFirstOrDefault]
        [UseProjection]
        public IQueryable<EventSpeaker> EventSpeaker { get; }
    }
}
