using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;
using Profile.Application.Queries;
using Profile.Domain.Entities;

namespace Profile.GraphQL.Schema
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Queries
    {
        [UseFirstOrDefault]
        [UseProjection]
        public async Task<IQueryable<PersonProfile>> GetPersonProfile([Service] IMediator mediator, Guid id)
        {
            return await mediator.Send(new GetPersonProfileFromGuidQuery(id));
        }
    }
}
