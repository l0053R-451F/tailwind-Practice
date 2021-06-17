using Profile.Domain.Common;

namespace Profile.Domain.Entities
{
    public class PersonProfile : AuditableEntity
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string OrganizationalRole { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string TwitterProfileUrl { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string GithubProfileUrl { get; set; }
        public string LinkedInProfileUrl { get; set; }

        public long OrganizationId { get; set; }
        public virtual OrganizationProfile Organization { get; set; }
    }
}
