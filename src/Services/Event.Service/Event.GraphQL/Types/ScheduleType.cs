using Event.Domain.Entities;
using HotChocolate.Types;

namespace Event.GraphQL.Types
{
    public class ScheduleType : ObjectType<Schedule>
    {
        protected override void Configure(IObjectTypeDescriptor<Schedule> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(e => e.EntityGuid)
                .Name("id")
                .Type<NonNullType<UuidType>>();
            descriptor.Field(e => e.Description)
                .Type<NonNullType<StringType>>();

            descriptor.Ignore(e => e.EventId);
            descriptor.Ignore(e => e.Id);
            descriptor.Ignore(e => e.CreatedAtUtc);
            descriptor.Ignore(e => e.CreatedBy);
            descriptor.Ignore(e => e.LastModifiedAtUtc);
            descriptor.Ignore(e => e.LastModifiedBy);
        }
    }
}
