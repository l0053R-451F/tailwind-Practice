using System;

namespace AdminApp.BlazorWasm.Models
{
    public class PushNotification
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTimeOffset IssuedAtUtc { get; set; }
    }
}
