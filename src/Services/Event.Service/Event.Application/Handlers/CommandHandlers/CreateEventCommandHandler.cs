using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Event.Application.Commands;
using Event.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Event.Application.Handlers.CommandHandlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Domain.Entities.Event>
    {
        private readonly IEventDataContext _context;

        public CreateEventCommandHandler(IEventDataContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var searchKey = request.Name.Trim().ToLower().Replace(' ', '-');
            var isDuplicateName = await _context.Events.FirstOrDefaultAsync(x => x.SearchKey == searchKey) != null;
            if (isDuplicateName) throw new ResponseException($"Duplicate Event name {request.Name}");
            var newEvent = new Domain.Entities.Event()
            {
                SearchKey = searchKey,
                Name = request.Name.Trim(),
                About = request.About.Trim(),
                Location = request.Place.Trim()
            };
            await _context.Events.AddAsync(newEvent, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (result) return newEvent;
            throw new ResponseException("Couldn't saved.'");
        }
    }
}
