using HotChocolate.Types;
using Profile.Domain.Entities;

namespace Profile.GraphQL.Types
{
    public class PersonProfileType : ObjectType<PersonProfile>
    {
        protected override void Configure(IObjectTypeDescriptor<PersonProfile> descriptor)
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
