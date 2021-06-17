using System;
using System.Linq;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Commands
{
    public class AddScheduleCommand : IRequest<IQueryable<Schedule>>
    {
        public Guid EventId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }
        public string ClientMutationId { get; set; }
    }
}
