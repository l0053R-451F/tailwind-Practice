using Profile.Domain.Common;
using System.Collections.Generic;

namespace Profile.Domain.Entities
{
    public class OrganizationProfile : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string FacebookLinkedIn { get; set; }
        public string LogoUrl { get; set; }

        public virtual ICollection<PersonProfile> Persons { get; set; }
    }
}
