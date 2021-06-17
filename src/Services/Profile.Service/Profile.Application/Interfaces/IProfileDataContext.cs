using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Profile.Domain.Entities;

namespace Profile.Application.Interfaces
{
    public interface IProfileDataContext
    {
        DbSet<PersonProfile> PersonProfiles { get; set; }
        DbSet<OrganizationProfile> OrganizationProfiles { get; set; }

        EntityEntry Attach([NotNull] object entity);
        EntityEntry Entry([NotNull] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
