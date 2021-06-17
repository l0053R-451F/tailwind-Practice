using EventRegistration.Domain.Entities;
using HotChocolate.Types;

namespace EventRegistration.GraphQL.Types
{
    public class RegistrationType : ObjectType<Registration>
    {
        protected override void Configure(IObjectTypeDescriptor<Registration> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(e => e.EntityGuid)
                .Name("id")
                .Type<UuidType>();

            descriptor.Ignore(e => e.Id);
            descriptor.Ignore(e => e.CreatedAtUtc);
            descriptor.Ignore(e => e.CreatedBy);
            descriptor.Ignore(e => e.LastModifiedAtUtc);
            descriptor.Ignore(e => e.LastModifiedBy);
        }
    }
}
