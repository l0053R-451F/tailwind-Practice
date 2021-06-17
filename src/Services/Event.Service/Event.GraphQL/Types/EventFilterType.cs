using HotChocolate.Data.Filters;

namespace Event.GraphQL.Types
{
    public class EventFilterType : FilterInputType<Domain.Entities.Event>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Domain.Entities.Event> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Ignore(e => e.FacebookEventUrl);
            descriptor.Ignore(e => e.MapUrl);
            descriptor.Ignore(e => e.Id);
            descriptor.Ignore(e => e.SearchKey);
            descriptor.Ignore(e => e.EntityGuid);
            descriptor.Ignore(e => e.CreatedAtUtc);
            descriptor.Ignore(e => e.CreatedBy);
            descriptor.Ignore(e => e.LastModifiedAtUtc);
            descriptor.Ignore(e => e.LastModifiedBy);
        }
    }
}
