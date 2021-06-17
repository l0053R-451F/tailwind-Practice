using HotChocolate.Types;

namespace Event.GraphQL.Types
{
    public class EventType : ObjectType<Domain.Entities.Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Domain.Entities.Event> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(e => e.SearchKey)
                .Name("id")
                .Type<NonNullType<StringType>>();

            descriptor.Ignore(e => e.Id);
            descriptor.Ignore(e => e.EntityGuid);
            descriptor.Ignore(e => e.CreatedAtUtc);
            descriptor.Ignore(e => e.CreatedBy);
            descriptor.Ignore(e => e.LastModifiedAtUtc);
            descriptor.Ignore(e => e.LastModifiedBy);
        }
    }
}
