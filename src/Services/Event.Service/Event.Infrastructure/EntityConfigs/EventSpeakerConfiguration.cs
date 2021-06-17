using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.Infrastructure.EntityConfigs
{
    internal class EventSpeakerConfiguration : IEntityTypeConfiguration<EventSpeaker>
    {
        public void Configure(EntityTypeBuilder<EventSpeaker> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EventId).IsRequired();
            builder.Property(x => x.SpeakerName).IsRequired();
            builder.Property(x => x.SpeakerProfileId).IsRequired();
            builder.HasOne(x => x.Event).WithMany(x => x.EventSpeakers).HasForeignKey(x => x.EventId);
        }
    }
}
