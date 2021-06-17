using EventRegistration.Application.Queries;
using EventRegistration.Domain.Entities;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventRegistration.GraphQL.Schema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Queries
    {
        [Authorize]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Registration>> GetEventRegistrations([Service] IMediator mediator, Guid eventId)
        {
            return await mediator.Send(new GetEventRegistrationsQuery(eventId));
        }
    }
}
