using System;
using System.Collections.Generic;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    public class Event : AuditableEntity
    {
        public long Id { get; set; }
        public string SearchKey { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Location { get; set; }
        public string MapUrl { get; set; }
        public string FacebookEventUrl { get; set; }
        public DateTimeOffset EventStartDateTimeUtc { get; set; }
        public DateTimeOffset EventEndDateTimeUtc { get; set; }
        public DateTimeOffset RegistrationStartDateTimeUtc { get; set; }
        public DateTimeOffset RegistrationEndDateTimeUtc { get; set; }
        public bool IsPublished { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<EventSpeaker> EventSpeakers { get; set; }
    }
}
