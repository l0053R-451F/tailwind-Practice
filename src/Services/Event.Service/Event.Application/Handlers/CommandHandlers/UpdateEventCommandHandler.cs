using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Event.Application.Commands;
using Event.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Event.Application.Handlers.CommandHandlers
{
    internal class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, IQueryable<Domain.Entities.Event>>
    {
        private readonly IEventDataContext _context;

        public UpdateEventCommandHandler(IEventDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Domain.Entities.Event>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EntityGuid == request.Id, cancellationToken);
            if (@event == null) throw new Exception("Not found.");
            if (request.Name != null) @event.Name = request.Name;
            if (request.About != null) @event.About = request.About;
            if (request.Place != null) @event.Location = request.Place;
            if (request.IsPublished != null) @event.IsPublished = (bool)request.IsPublished;
            var success = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (!success) throw new Exception("Not updated.");
            return _context.Events.Where(e => e.EntityGuid == request.Id);
        }
    }
}
