using EventRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace EventRegistration.Application.Interfaces
{
    public interface IEventRegistrationDataContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<Registration> EventRegistrations { get; set; }

        EntityEntry Attach([NotNull] object entity);
        EntityEntry Entry([NotNull] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
