using System.Collections.Generic;

namespace EventRegistration.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        public string UserEmail { get; }
        public string PhoneNumber { get; }
        public string UserName { get; }
        public string RequestOrigin { get; }
        public List<string> Roles { get; }
        public bool IsAuthenticated { get; }
    }
}
