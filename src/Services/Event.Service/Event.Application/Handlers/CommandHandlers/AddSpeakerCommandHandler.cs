using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using Event.Application.Commands;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Event.Application.Handlers.CommandHandlers
{
    internal class AddSpeakerCommandHandler : IRequestHandler<AddSpeakerCommand, IQueryable<EventSpeaker>>
    {
        private readonly IEventDataContext _context;
        private readonly IMapper _mapper;

        public AddSpeakerCommandHandler(IEventDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<EventSpeaker>> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            var @event =
                await _context.Events.FirstOrDefaultAsync(e => e.EntityGuid == request.EventId, cancellationToken);
            if (@event == null) throw new ResponseException("Event not found");
            var alreadyAdded = await _context.EventSpeakers.FirstOrDefaultAsync(
                                   es => es.EventId == @event.Id && es.SpeakerProfileId == request.SpeakerProfileId,
                                   cancellationToken) != null;
            if (alreadyAdded) throw new ResponseException("Speaker Already Added.");
            var eventSpeaker = _mapper.Map<EventSpeaker>(request);
            eventSpeaker.EventId = @event.Id;
            await _context.EventSpeakers.AddAsync(eventSpeaker, cancellationToken);
            var succeeded = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (succeeded)
                return _context.EventSpeakers.Where(es =>
                    es.EventId == @event.Id && es.SpeakerProfileId == request.SpeakerProfileId);
            throw new ResponseException("Couldn't add.'");
        }
    }
}
