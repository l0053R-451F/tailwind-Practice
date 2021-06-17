using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Application.Queries;
using MediatR;

namespace Event.Application.Handlers.QueryHandlers
{
    internal class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IQueryable<Domain.Entities.Event>>
    {
        private readonly IEventDataContext _context;

        public GetEventsQueryHandler(IEventDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Domain.Entities.Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context.Events, cancellationToken);
        }
    }
}
