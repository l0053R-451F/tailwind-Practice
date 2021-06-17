using Common.Exceptions;
using EventRegistration.Application.Interfaces;
using EventRegistration.Application.Queries;
using EventRegistration.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventRegistration.Application.Handlers.QueryHandlers
{
    internal class GetEventRegistrationsQueryHandler : IRequestHandler<GetEventRegistrationsQuery, IQueryable<Registration>>
    {
        private readonly IEventRegistrationDataContext _context;

        public GetEventRegistrationsQueryHandler(IEventRegistrationDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Registration>> Handle(GetEventRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EntityGuid == request.EventId, cancellationToken);
            if (@event == null) throw new ResponseException("Event not found");

            return await Task.Run(() => _context.EventRegistrations.Where(er => er.EventId == @event.Id), cancellationToken);
        }
    }
}
