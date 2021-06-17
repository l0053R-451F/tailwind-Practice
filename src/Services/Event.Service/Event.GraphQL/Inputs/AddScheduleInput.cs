using Event.Application.Commands;
using HotChocolate.Types;

namespace Event.GraphQL.Inputs
{
    public class AddScheduleInput : InputObjectType<AddScheduleCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddScheduleCommand> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(c => c.EventId)
                .Type<NonNullType<UuidType>>();
            descriptor.Field(c => c.Description)
                .Type<NonNullType<StringType>>();
            descriptor.Field(c => c.StartDateTime)
                .Type<NonNullType<DateTimeType>>();
            descriptor.Field(c => c.EndDateTime)
                .Type<DateTimeType>();
        }
    }
}
