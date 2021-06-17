using EventRegistration.Application.Commands;
using HotChocolate.Types;

namespace EventRegistration.GraphQL.Inputs
{
    public class RegistrationInput : InputObjectType<EventRegistrationCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EventRegistrationCommand> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(c => c.EventId)
                .Type<NonNullType<UuidType>>();
            descriptor.Field(c => c.Name)
                .Type<NonNullType<StringType>>();
            descriptor.Field(c => c.PhoneNumber)
                .Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Email)
                .Type<NonNullType<StringType>>();
        }
    }
}
