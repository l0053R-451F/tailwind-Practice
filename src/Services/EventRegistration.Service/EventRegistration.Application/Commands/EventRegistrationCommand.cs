using MediatR;
using System;

namespace EventRegistration.Application.Commands
{
    public class EventRegistrationCommand : IRequest<bool>
    {
        public EventRegistrationCommand(Guid eventId, string name, string phoneNumber, string email, string profession, string organization)
        {
            EventId = eventId;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Profession = profession;
            Organization = organization;
        }

        public Guid EventId { get; }
        public string Name { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string Profession { get; }
        public string Organization { get; }
    }
}
