using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infrastructure.DbContexts
{
    public class EventRegistrationDataContext : DbContext, IEventRegistrationDataContext
    {
        private readonly ICurrentUserService _currentUserService;

        public EventRegistrationDataContext(DbContextOptions<EventRegistrationDataContext> options, ICurrentUserService currentUserService) : base(options)
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
        public DbSet<Domain.Entities.Registration> EventRegistrations { get; set; }

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
