using System.Linq;
using Event.Domain.Entities;
using HotChocolate.Data;

namespace Event.GraphQL.Payloads
{
    public class AddSchedulePayload
    {
        public AddSchedulePayload(IQueryable<Schedule> schedule, string clientMutationId)
        {
            Schedule = schedule;
            ClientMutationId = clientMutationId;
        }

        [UseFirstOrDefault]
        [UseProjection]
        public IQueryable<Schedule> Schedule { get; private set; }
        public string ClientMutationId { get; private set; }
    }
}
