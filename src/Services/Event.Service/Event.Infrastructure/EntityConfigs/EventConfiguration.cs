using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure.EntityConfigs
{
    internal class EventConfiguration : IEntityTypeConfiguration<Domain.Entities.Event>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.SearchKey).IsUnique();
            builder.Property(x => x.SearchKey).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.About).IsRequired();
            builder.Property(x => x.EventStartDateTimeUtc).IsRequired();
            builder.Property(x => x.EventEndDateTimeUtc).IsRequired();
            builder.Property(x => x.IsPublished).HasDefaultValue(false);
        }
    }
}
