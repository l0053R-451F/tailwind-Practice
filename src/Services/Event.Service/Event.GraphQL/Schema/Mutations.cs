using System.Threading.Tasks;
using Event.Application.Commands;
using Event.GraphQL.Payloads;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Event.GraphQL.Schema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Mutations
    {
        //[Authorize]
        public async Task<CreateEventPayload> CreateEventAsync([Service] IHttpContextAccessor contextAccessor, [Service] IMediator mediator,
            [GraphQLNonNullType] CreateEventCommand input)
        {
            var @event = await mediator.Send(input);
            return new CreateEventPayload(@event, input.ClientMutationId);
        }

        //public async Task<AddSchedulePayload> AddScheduleAsync([Service] IMediator mediator,
        //    [GraphQLNonNullType] AddScheduleCommand input)
        //{
        //    var schedule = await mediator.Send(input);
        //    return new AddSchedulePayload(schedule, input.ClientMutationId);
        //}

        public async Task<UpdateEventPayload> UpdateEvent([Service] IMediator mediator,
            [GraphQLNonNullType] UpdateEventCommand input)
        {
            var updatedEvent = await mediator.Send(input);
            return new UpdateEventPayload(updatedEvent, input.ClientMutationId);
        }

        public async Task<AddSpeakerPayload> AddSpeaker([Service] IMediator mediator,
            [GraphQLNonNullType] AddSpeakerCommand input)
        {
            var eventSpeaker = await mediator.Send(input);
            return new AddSpeakerPayload(input.ClientMutationId, eventSpeaker);
        }
    }
}
