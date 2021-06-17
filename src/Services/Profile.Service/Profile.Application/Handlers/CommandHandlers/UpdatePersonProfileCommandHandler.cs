using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profile.Application.Commands;
using Profile.Application.Interfaces;
using Profile.Domain.Entities;

namespace Profile.Application.Handlers.CommandHandlers
{
    internal class UpdatePersonProfileCommandHandler : IRequestHandler<UpdatePersonProfileCommand, IQueryable<PersonProfile>>
    {
        private readonly IProfileDataContext _context;

        public UpdatePersonProfileCommandHandler(IProfileDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<PersonProfile>> Handle(UpdatePersonProfileCommand request, CancellationToken cancellationToken)
        {
            var profile =
                await _context.PersonProfiles.FirstOrDefaultAsync(pp => pp.EntityGuid == request.Id, cancellationToken);
            if (profile == null) throw new ResponseException("Profile not found.");

            if (!string.IsNullOrEmpty(request.Name)) profile.Name = request.Name;
            //TODO: if (!string.IsNullOrEmpty(request.Organization)) profile.Organization = request.Organization;
            if (!string.IsNullOrEmpty(request.OrganizationalRole)) profile.OrganizationalRole = request.OrganizationalRole;
            if (!string.IsNullOrEmpty(request.PhoneNumber)) profile.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.TwitterProfileUrl)) profile.TwitterProfileUrl = request.TwitterProfileUrl;
            if (!string.IsNullOrEmpty(request.FacebookProfileUrl)) profile.FacebookProfileUrl = request.FacebookProfileUrl;
            if (!string.IsNullOrEmpty(request.GithubProfileUrl)) profile.GithubProfileUrl = request.GithubProfileUrl;
            if (!string.IsNullOrEmpty(request.LinkedInProfileUrl)) profile.LinkedInProfileUrl = request.LinkedInProfileUrl;

            var succeed = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (succeed) return _context.PersonProfiles.Where(pp => pp.Id == profile.Id);
            throw new ResponseException("Something wrong...");
        }
    }
}