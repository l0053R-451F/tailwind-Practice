using Common.Exceptions;
using EventRegistration.Application.Commands;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EventRegistration.Application.Handlers.CommandHandlers
{
    internal class EventRegistrationCommandHandler : IRequestHandler<EventRegistrationCommand, bool>
    {
        private readonly IEventRegistrationDataContext _context;

        public EventRegistrationCommandHandler(IEventRegistrationDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EventRegistrationCommand request, CancellationToken cancellationToken)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EntityGuid == request.EventId,
                cancellationToken: cancellationToken);
            if (@event == null) throw new ResponseException("Event not found.");
            if (!@event.IsPublished) throw new ResponseException("Event not published.");

            var registration =
                await _context.EventRegistrations.FirstOrDefaultAsync(
                    r => r.Email == request.Email || r.PhoneNumber == request.PhoneNumber, cancellationToken);
            if (registration != null) throw new ResponseException("Already registered with same email or phone.");

            registration = new Registration()
            {
                EventId = @event.Id,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Profession = request.Profession,
                Organization = request.Profession,
                IsConfirmed = false
            };

            await _context.EventRegistrations.AddAsync(registration, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
