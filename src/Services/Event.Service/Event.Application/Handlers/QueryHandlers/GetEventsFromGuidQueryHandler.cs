using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Application.Queries;
using MediatR;

namespace Event.Application.Handlers.QueryHandlers
{
    class GetEventsFromGuidQueryHandler : IRequestHandler<GetEventsFromGuidQuery, IQueryable<Domain.Entities.Event>>
    {
        private readonly IEventDataContext _context;

        public GetEventsFromGuidQueryHandler(IEventDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Domain.Entities.Event>> Handle(GetEventsFromGuidQuery request, CancellationToken cancellationToken)
        {
            var random = new Random();
            int randomnumber = random.Next();
            return await Task.Run(() => _context.Events.Where(e => e.EntityGuid == request.Guid), cancellationToken);
        }
    }
}
