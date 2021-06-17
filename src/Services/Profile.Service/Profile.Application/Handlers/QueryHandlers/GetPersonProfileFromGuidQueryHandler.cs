using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Interfaces;
using Profile.Application.Queries;
using Profile.Domain.Entities;

namespace Profile.Application.Handlers.QueryHandlers
{
    internal class GetPersonProfileFromGuidQueryHandler : IRequestHandler<GetPersonProfileFromGuidQuery, IQueryable<PersonProfile>>
    {
        private readonly IProfileDataContext _context;

        public GetPersonProfileFromGuidQueryHandler(IProfileDataContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<PersonProfile>> Handle(GetPersonProfileFromGuidQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context.PersonProfiles.Where(pp => pp.EntityGuid == request.Guid), cancellationToken);
        }
    }
}
