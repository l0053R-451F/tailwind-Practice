using System;
using System.Linq;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Commands
{
    public class AddSpeakerCommand : IRequest<IQueryable<EventSpeaker>>
    {
        public string ClientMutationId { get; set; }
        public Guid EventId { get; set; }
        public Guid SpeakerProfileId { get; set; }
        public string SpeakerName { get; set; }
        public string Topic { get; set; }
    }
}
