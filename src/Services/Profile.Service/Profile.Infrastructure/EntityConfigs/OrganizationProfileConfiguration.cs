using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Entities;

namespace Profile.Infrastructure.EntityConfigs
{
    internal class OrganizationProfileConfiguration : IEntityTypeConfiguration<OrganizationProfile>
    {
        public void Configure(EntityTypeBuilder<OrganizationProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
