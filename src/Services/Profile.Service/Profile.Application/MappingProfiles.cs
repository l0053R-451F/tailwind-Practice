using Profile.Application.Commands;
using Profile.Domain.Entities;

namespace Profile.Application
{
    internal class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreatePersonProfileCommand, PersonProfile>();
        }
    }
}
