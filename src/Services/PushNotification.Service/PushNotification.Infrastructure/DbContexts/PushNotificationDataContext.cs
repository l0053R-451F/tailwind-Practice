using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PushNotification.Application.Interfaces;
using PushNotification.Domain.Common;
using PushNotification.Domain.Entities;

namespace PushNotification.Infrastructure.DbContexts
{
    public class PushNotificationDataContext : DbContext, IPushNotificationDataContext
    {
        public PushNotificationDataContext(DbContextOptions<PushNotificationDataContext> options) : base(options)
        {
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

        public DbSet<Notification> NotificationLog { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = null;
                        entry.Entity.CreatedAtUtc = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = null;
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
