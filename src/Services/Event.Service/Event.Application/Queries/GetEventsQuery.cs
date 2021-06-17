using System.Linq;
using MediatR;

namespace Event.Application.Queries
{
    public class GetEventsQuery : IRequest<IQueryable<Domain.Entities.Event>>
    {

    }
}
