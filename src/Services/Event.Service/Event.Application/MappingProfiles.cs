using Event.Application.Commands;
using Event.Domain.Entities;

namespace Event.Application
{
    internal class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddSpeakerCommand, EventSpeaker>();
        }
    }
}
