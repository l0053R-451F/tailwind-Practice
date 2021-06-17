using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Event.Application.Interfaces
{
    public interface IEventDataContext
    {
        DbSet<Domain.Entities.Event> Events { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<EventSpeaker> EventSpeakers { get; set; }

        EntityEntry Attach([NotNull] object entity);
        EntityEntry Entry([NotNull] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
