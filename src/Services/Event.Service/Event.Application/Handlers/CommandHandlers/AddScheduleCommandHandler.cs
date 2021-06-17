using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Event.Application.Commands;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Event.Application.Handlers.CommandHandlers
{
    public class AddScheduleCommandHandler : IRequestHandler<AddScheduleCommand, IQueryable<Schedule>>
    {
        private readonly IEventDataContext _context;

        public AddScheduleCommandHandler(IEventDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Schedule>> Handle(AddScheduleCommand request, CancellationToken cancellationToken)
        {
            var @event =
                await _context.Events.FirstOrDefaultAsync(e => e.EntityGuid == request.EventId, cancellationToken);
            if (@event == null) throw new ResponseException("Event not found");

            var overlappingSchedulesCount = request.EndDateTime != null
                ? await _context.Schedules
                    .Where(s => s.EventId == @event.Id
                                && ((request.StartDateTime <= s.EndDateTimeUtc && request.EndDateTime <= s.EndDateTimeUtc)
                                    || (request.StartDateTime >= s.EndDateTimeUtc && request.EndDateTime <= s.EndDateTimeUtc)
                                    || (request.StartDateTime >= s.EndDateTimeUtc &&
                                        request.EndDateTime >= s.EndDateTimeUtc)))
                    .CountAsync(cancellationToken)
                : await _context.Schedules.Where(s => s.EventId == @event.Id && request.StartDateTime == s.StartDateTimeUtc)
                    .CountAsync(cancellationToken);

            if (overlappingSchedulesCount > 0)
            {
                throw new ResponseException("Schedule overlapping");
            }

            var newSchedule = new Schedule()
            {
                EventId = @event.Id,
                Description = request.Description,
                StartDateTimeUtc = request.StartDateTime,
                EndDateTimeUtc = request.EndDateTime,
            };

            await _context.Schedules.AddAsync(newSchedule, cancellationToken);
            var success = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (!success) throw new ResponseException("Schedule couldn't save");
            return _context.Schedules.Where(s => s.Id == newSchedule.Id);
        }
    }
}
