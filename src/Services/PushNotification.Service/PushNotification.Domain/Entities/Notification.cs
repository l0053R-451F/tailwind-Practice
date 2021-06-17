using PushNotification.Domain.Common;
using System;

namespace PushNotification.Domain.Entities
{
    public class Notification : AuditableEntity
    {
        public long Id { get; set; }
        public string AudienceUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsSuccessfullyDelivered { get; set; }
        public DateTimeOffset DeliveredAtUtc { get; set; }
    }
}
