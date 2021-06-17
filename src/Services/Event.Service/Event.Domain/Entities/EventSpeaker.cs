using System;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    public class EventSpeaker : AuditableEntity
    {
        public long Id { get; set; }
        public Guid SpeakerProfileId { get; set; }
        public string SpeakerName { get; set; }

        public long EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
