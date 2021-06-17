using HotChocolate.Types;
using Profile.Application.Commands;

namespace Profile.GraphQL.Inputs
{
    public class UpdatePersonProfileInput : InputObjectType<UpdatePersonProfileCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdatePersonProfileCommand> descriptor)
        {
            base.Configure(descriptor);
        }
    }
}
