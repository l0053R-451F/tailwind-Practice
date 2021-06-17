using Auth.IdentityServer.Oidc.Web.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.IdentityServer.Oidc.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base
                .ChangeTracker.Entries<IAuditable>()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified))
            {
                if (entry.State != EntityState.Added)
                {
                    entry.Entity.LastModifiedAt = DateTimeOffset.UtcNow;
                    //entry.Entity.UpdatedBy = entry.Entity.UpdatedBy ?? userId;
                }
                else
                {
                    entry.Entity.CreatedAtUtc = DateTimeOffset.UtcNow;
                    //entry.Entity.CreatedBy = entry.Entity.CreatedBy != 0 ? entry.Entity.CreatedBy : userId;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
