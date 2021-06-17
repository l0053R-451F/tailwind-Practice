using System;
using System.Linq;
using MediatR;

namespace Event.Application.Queries
{
    public class GetEventsFromGuidQuery : IRequest<IQueryable<Domain.Entities.Event>>
    {
        public GetEventsFromGuidQuery(Guid guid)
        {
            Guid = guid;
        }

        public Guid Guid { get; }
    }
}
