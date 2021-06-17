using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Domain.Entities;

namespace Profile.Infrastructure.EntityConfigs
{
    internal class PersonProfileConfiguration : IEntityTypeConfiguration<PersonProfile>
    {
        public void Configure(EntityTypeBuilder<PersonProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x => x.Organization).WithMany(x => x.Persons).HasForeignKey(x => x.OrganizationId);
        }
    }
}
