using System.Linq;
using MediatR;
using Profile.Domain.Entities;

namespace Profile.Application.Commands
{
    public class CreatePersonProfileCommand : IRequest<IQueryable<PersonProfile>>
    {
        public string ClientMutationId { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string OrganizationalRole { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string TwitterProfileUrl { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string GithubProfileUrl { get; set; }
        public string LinkedInProfileUrl { get; set; }
    }
}
