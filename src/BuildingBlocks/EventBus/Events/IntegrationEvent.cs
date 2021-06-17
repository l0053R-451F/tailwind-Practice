using System;

namespace EventBus.Events
{
    public class IntegrationEvent
    {
        public Guid Id { get;}
        public DateTimeOffset CreateAtUtc { get; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreateAtUtc = DateTimeOffset.UtcNow;
        }
    }
}
