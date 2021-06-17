using System;
using System.Linq;
using MediatR;

namespace Event.Application.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UpdateEventCommand : IRequest<IQueryable<Domain.Entities.Event>>
    {
        public UpdateEventCommand(Guid id, string name, string about, string place, bool? isPublished, string clientMutationId)
        {
            Id = id;
            Name = name;
            About = about;
            Place = place;
            IsPublished = isPublished;
            ClientMutationId = clientMutationId;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string About { get; }
        public string Place { get; }
        public bool? IsPublished { get; }
        public string ClientMutationId { get; }
    }
}
