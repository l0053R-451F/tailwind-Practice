using System;

namespace Auth.IdentityServer.Oidc.Web.Entities
{
    interface IAuditable
    {
        public Guid EntityGuid { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
