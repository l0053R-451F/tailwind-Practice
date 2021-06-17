using System;
using System.Linq;
using MediatR;
using Profile.Domain.Entities;

namespace Profile.Application.Commands
{
    public class UpdatePersonProfileCommand : IRequest<IQueryable<PersonProfile>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string OrganizationalRole { get; set; }
        public string PhoneNumber { get; set; }
        public string TwitterProfileUrl { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string GithubProfileUrl { get; set; }
        public string LinkedInProfileUrl { get; set; }

        public string ClientMutationId { get; set; }
    }
}