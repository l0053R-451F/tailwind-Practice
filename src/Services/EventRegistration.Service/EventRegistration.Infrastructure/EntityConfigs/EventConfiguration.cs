using EventRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventRegistration.Infrastructure.EntityConfigs
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EventServiceEntityGuid).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.RegistrationStartDateTimeUtc).IsRequired();
            builder.Property(x => x.RegistrationEndDateTimeUtc).IsRequired();
            builder.Property(x => x.IsPublished).IsRequired();
        }
    }
}
