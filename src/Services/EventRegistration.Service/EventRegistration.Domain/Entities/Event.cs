using System;
using System.Collections.Generic;
using EventRegistration.Domain.Common;

namespace EventRegistration.Domain.Entities
{
    public class Event : AuditableEntity
    {
        public long Id { get; set; }
        public Guid EventServiceEntityGuid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset RegistrationStartDateTimeUtc { get; set; }
        public DateTimeOffset RegistrationEndDateTimeUtc { get; set; }
        public bool IsPublished { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
