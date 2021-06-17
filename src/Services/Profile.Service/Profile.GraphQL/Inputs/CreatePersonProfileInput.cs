using HotChocolate.Types;
using Profile.Application.Commands;

namespace Profile.GraphQL.Inputs
{
    public class CreatePersonProfileInput : InputObjectType<CreatePersonProfileCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreatePersonProfileCommand> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(pp => pp.Name)
                .Type<NonNullType<StringType>>();
            descriptor.Field(pp => pp.EmailAddress)
                .Type<NonNullType<StringType>>();
        }
    }
}