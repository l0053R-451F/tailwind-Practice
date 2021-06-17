using PushNotification.Application.Interfaces;
using PushNotification.Domain.Entities;
using System.Threading.Tasks;

namespace PushNotification.Infrastructure.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly IPushNotificationDataContext _context;

        public NotificationService(IPushNotificationDataContext context)
        {
            _context = context;
        }

        public async Task PersistNotificationAsync(Notification notification)
        {
            _context.NotificationLog.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
