using PushNotification.Domain.Entities;
using System.Threading.Tasks;

namespace PushNotification.Application.Interfaces
{
    public interface INotificationService
    {
        Task PersistNotificationAsync(Notification notification);
    }
}
