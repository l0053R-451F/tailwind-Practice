namespace AdminApp.BlazorWasm.Helpers
{
    public class AppSettings
    {
        public IdentityServer IdentityServer { get; set; }
        public Service[] Services { get; set; }
        public string BradcastHubUrl { get; set; }
        public string EchoHubUrl { get; set; }
    }

    public class IdentityServer
    {
        public string ClientId { get; set; }
        public string Authority { get; set; }
        public string[] DefaultScopes { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string ResponseType { get; set; }
    }
    public class Service
    {
        public string Name { get; set; }
        public string BaseUri { get; set; }
    }
}
