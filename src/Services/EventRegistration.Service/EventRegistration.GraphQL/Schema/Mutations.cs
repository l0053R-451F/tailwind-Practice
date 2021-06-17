using EventRegistration.Application.Commands;
using EventRegistration.GraphQL.Payloads;
using HotChocolate;
using MediatR;
using System.Threading.Tasks;

namespace EventRegistration.GraphQL.Schema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Mutations
    {
        public async Task<EventRegistrationPayload> Registration([Service] IMediator mediator,
            EventRegistrationCommand input)
        {
            var result = await mediator.Send(input);
            return result
                ? new EventRegistrationPayload(true, "Successful")
                : new EventRegistrationPayload(false, "Unsuccessful");
        }
    }
}
