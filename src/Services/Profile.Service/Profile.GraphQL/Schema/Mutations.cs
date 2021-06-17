using System.Threading.Tasks;
using HotChocolate;
using MediatR;
using Profile.Application.Commands;
using Profile.GraphQL.Payloads;

namespace Profile.GraphQL.Schema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Mutations
    {
        public async Task<CreatePersonProfilePayload> CreatePersonProfileAsync([Service] IMediator mediator, [GraphQLNonNullType] CreatePersonProfileCommand input)
        {
            var profile = await mediator.Send(input);
            return new CreatePersonProfilePayload(input.ClientMutationId, profile);
        }

        public async Task<UpdatePersonProfilePayload> UpdatePersonProfileAsync([Service] IMediator mediator, [GraphQLNonNullType] UpdatePersonProfileCommand input)
        {
            var profile = await mediator.Send(input);
            return new UpdatePersonProfilePayload(input.ClientMutationId, profile);
        }
    }
}
