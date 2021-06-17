using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PushNotification.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace PushNotification.Application.Interfaces
{
    public interface IPushNotificationDataContext
    {
        DbSet<Notification> NotificationLog { get; set; }

        EntityEntry Attach([NotNull] object entity);
        EntityEntry Entry([NotNull] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
