using EventRegistration.Domain.Common;

namespace EventRegistration.Domain.Entities
{
    public class Registration : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
        public string Organization { get; set; }
        public bool IsConfirmed { get; set; }

        public long EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
