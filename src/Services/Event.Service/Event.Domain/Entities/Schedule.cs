using System;
using Event.Domain.Common;

namespace Event.Domain.Entities
{
    public class Schedule : AuditableEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PerformerName { get; set; }
        public Guid? PerformerProfileId { get; set; }
        public DateTimeOffset StartDateTimeUtc { get; set; }
        public DateTimeOffset? EndDateTimeUtc { get; set; }

        public long EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
