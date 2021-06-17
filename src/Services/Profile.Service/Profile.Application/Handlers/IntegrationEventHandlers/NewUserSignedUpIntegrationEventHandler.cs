using EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Profile.Application.Interfaces;
using Profile.Application.IntegrationEvents;
using Profile.Domain.Entities;
using System.Threading.Tasks;

namespace Profile.Application.Handlers.IntigrationEventHandlers
{
    class NewUserSignedUpIntegrationEventHandler : IIntegrationEventHandler<NewUserSignedUpIntegrationEvent>
    {
        private readonly ILogger<NewUserSignedUpIntegrationEventHandler> _logger;
        private readonly IProfileDataContext _context;

        public NewUserSignedUpIntegrationEventHandler(IProfileDataContext context, ILogger<NewUserSignedUpIntegrationEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task HandleAsync(NewUserSignedUpIntegrationEvent @event)
        {
            var profile = await _context.PersonProfiles.FirstOrDefaultAsync(p => p.EmailAddress == @event.Email);
            if(profile == null)
            {
                _logger.LogInformation($"Profile not found for UserId: {@event.UserId}. Creating one...");
                profile = new PersonProfile()
                {
                    EmailAddress = @event.Email
                };
            }
            profile.UserId = @event.UserId;
            var result = await _context.SaveChangesAsync() > 0;
            if(result)
            {
                _logger.LogInformation($"Updated UserId: {@event.UserId} in Profile...");
                return;
            }

            _logger.LogError($"Couldn't create Profile for UserId: {@event.UserId}");
            return;
        }
    }
}
