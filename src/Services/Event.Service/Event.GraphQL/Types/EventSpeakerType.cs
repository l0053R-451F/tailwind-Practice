using Event.Domain.Entities;
using HotChocolate.Types;

namespace Event.GraphQL.Types
{
    public class EventSpeakerType : ObjectType<EventSpeaker>
    {
        protected override void Configure(IObjectTypeDescriptor<EventSpeaker> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(e => e.EntityGuid)
                .Name("id")
                .Type<NonNullType<UuidType>>();

            descriptor.Ignore(e => e.Id);
            descriptor.Ignore(e => e.CreatedAtUtc);
            descriptor.Ignore(e => e.CreatedBy);
            descriptor.Ignore(e => e.LastModifiedAtUtc);
            descriptor.Ignore(e => e.LastModifiedBy);
        }
    }
}
