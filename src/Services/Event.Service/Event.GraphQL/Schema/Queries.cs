using System;
using System.Linq;
using System.Threading.Tasks;
using Event.Application.Queries;
using Event.GraphQL.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;

namespace Event.GraphQL.Schema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Queries
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering(typeof(EventFilterType))]
        [UseSorting]
        public async Task<IQueryable<Domain.Entities.Event>> GetEvents([Service] IMediator mediator)
        {
            return await mediator.Send(new GetEventsQuery());
        }

        [UseFirstOrDefault]
        [UseProjection]
        public async Task<IQueryable<Domain.Entities.Event>> GetEvent([Service] IMediator mediator, Guid id)
        {
            return await mediator.Send(new GetEventsFromGuidQuery(id));
        }
    }
}
