using System.Linq;
using HotChocolate.Data;
using HotChocolate.Types;
using Profile.Domain.Entities;

namespace Profile.GraphQL.Payloads
{
    public class UpdatePersonProfilePayload
    {
        public UpdatePersonProfilePayload(string clientMutationId, IQueryable<PersonProfile> personProfile)
        {
            ClientMutationId = clientMutationId;
            PersonProfile = personProfile;
        }

        public string ClientMutationId { get; }
        [UseFirstOrDefault]
        [UseProjection]
        public IQueryable<PersonProfile> PersonProfile { get; }
    }
}
