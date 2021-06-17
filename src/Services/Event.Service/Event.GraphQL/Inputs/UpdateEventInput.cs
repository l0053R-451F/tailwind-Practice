using Event.Application.Commands;
using HotChocolate.Types;

namespace Event.GraphQL.Inputs
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UpdateEventInput : InputObjectType<UpdateEventCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateEventCommand> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(c => c.Id)
                .Type<NonNullType<UuidType>>();
        }
    }
}
