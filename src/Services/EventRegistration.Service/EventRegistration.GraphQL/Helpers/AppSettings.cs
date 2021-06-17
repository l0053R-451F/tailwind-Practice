namespace EventRegistration.GraphQL.Helpers
{
    public class AppSettings
    {
        public IdentityServerConfig IdentityServerConfig { get; set; }
    }

    public class IdentityServerConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
