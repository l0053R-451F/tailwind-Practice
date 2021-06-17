using System;

namespace Event.Domain.Common
{
    public class AuditableEntity
    {
        public Guid EntityGuid { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedAtUtc { get; set; }
    }
}
