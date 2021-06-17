using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Common;
using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure.DbContexts
{
    public class EventDataContext : DbContext, IEventDataContext
    {
        private readonly ICurrentUserService _currentUserService;

        public EventDataContext(DbContextOptions<EventDataContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //For auto generate Guid Id...
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                foreach (var property in entity.ClrType.GetProperties()
                    .Where(p => p.Name == "EntityGuid" && p.PropertyType == typeof(Guid)))
                {
                    builder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("newsequentialid()");
                }
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Domain.Entities.Event> Events { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedAtUtc = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedAtUtc = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
