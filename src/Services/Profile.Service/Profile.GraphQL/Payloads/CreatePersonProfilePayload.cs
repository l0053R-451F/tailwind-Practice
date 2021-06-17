using System.Linq;
using Profile.Domain.Entities;

namespace Profile.GraphQL.Payloads
{
    public class CreatePersonProfilePayload
    {
        public CreatePersonProfilePayload(string clientMutationId, IQueryable<PersonProfile> personProfile)
        {
            ClientMutationId = clientMutationId;
            PersonProfile = personProfile;
        }

        public string ClientMutationId { get; }
        public IQueryable<PersonProfile> PersonProfile { get; }
    }
}
