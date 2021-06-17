using MediatR;

namespace Event.Application.Commands
{
    public class CreateEventCommand : IRequest<Domain.Entities.Event>
    {
        public CreateEventCommand(string name, string about, string place, string clientMutationId)
        {
            Name = name;
            About = about;
            Place = place;
            ClientMutationId = clientMutationId;
        }

        public string Name { get; }
        public string About { get; }
        public string Place { get; }
        public string ClientMutationId { get; }
    }
}
