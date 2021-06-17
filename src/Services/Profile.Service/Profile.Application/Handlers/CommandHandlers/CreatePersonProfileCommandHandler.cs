using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profile.Application.Commands;
using Profile.Application.Interfaces;
using Profile.Domain.Entities;

namespace Profile.Application.Handlers.CommandHandlers
{
    internal class CreatePersonProfileCommandHandler : IRequestHandler<CreatePersonProfileCommand, IQueryable<PersonProfile>>
    {
        private readonly IProfileDataContext _context;
        private readonly IMapper _mapper;

        public CreatePersonProfileCommandHandler(IProfileDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<PersonProfile>> Handle(CreatePersonProfileCommand request, CancellationToken cancellationToken)
        {
            var duplicateEmail =
                await _context.PersonProfiles.FirstOrDefaultAsync(pp => pp.EmailAddress == request.EmailAddress,
                    cancellationToken) != null;
            if (duplicateEmail) throw new ResponseException("Email already entered.");

            var profile = _mapper.Map<PersonProfile>(request);
            await _context.PersonProfiles.AddAsync(profile, cancellationToken);
            var success = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (success) return _context.PersonProfiles.Where(pp => pp.EmailAddress == request.EmailAddress);
            throw new ResponseException("Profile not saved.");
        }
    }
}
