using System;
using System.Linq;
using MediatR;
using Profile.Domain.Entities;

namespace Profile.Application.Queries
{
    public class GetPersonProfileFromGuidQuery : IRequest<IQueryable<PersonProfile>>
    {
        public GetPersonProfileFromGuidQuery(Guid guid)
        {
            Guid = guid;
        }

        public Guid Guid { get; }
    }
}
