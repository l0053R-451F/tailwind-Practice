using EventRegistration.Domain.Entities;
using MediatR;
using System;
using System.Linq;

namespace EventRegistration.Application.Queries
{
    public class GetEventRegistrationsQuery : IRequest<IQueryable<Registration>>
    {
        public GetEventRegistrationsQuery(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; }
    }
}
