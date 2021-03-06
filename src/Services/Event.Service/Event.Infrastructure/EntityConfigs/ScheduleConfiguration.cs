using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.Infrastructure.EntityConfigs
{
    internal class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EventId).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.StartDateTimeUtc).IsRequired();
            builder.HasOne(x => x.Event).WithMany(x => x.Schedules).HasForeignKey(x => x.EventId);
        }
    }
}
