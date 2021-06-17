namespace EventRegistration.GraphQL.Payloads
{
    public class EventRegistrationPayload
    {
        public EventRegistrationPayload(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
