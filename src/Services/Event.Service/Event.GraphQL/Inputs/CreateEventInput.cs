using Event.Application.Commands;
using HotChocolate.Types;

namespace Event.GraphQL.Inputs
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CreateEventInput : InputObjectType<CreateEventCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateEventCommand> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(c => c.ClientMutationId)
                .Type<StringType>();
            descriptor.Field(c => c.Name)
                .Type<NonNullType<StringType>>();
            descriptor.Field(c => c.About)
                .Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Place)
                .Type<NonNullType<StringType>>();
        }
    }
}
