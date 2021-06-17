using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Auth.IdentityServer.Oidc.Web.Configs
{
    internal static class InMemoryDataSet
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("event.graph", "Event GraphQL")
                {
                    Scopes = { "event.graph" }
                },
                new ApiResource("event.registration.graph", "Event Registration GraphQL")
                {
                    Scopes = { "event.registration.graph" }
                },
                new ApiResource("profile.graph", "Profile GraphQL")
                {
                    Scopes = { "profile.graph" }
                },
                new ApiResource("pushnotification.signalr", "Push Notification SignalR")
                {
                    Scopes = { "pushnotification.signalr" }
                }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("event.graph", "Event GraphQL"),
                new ApiScope("event.registration.graph", "Event Registration GraphQL"),
                new ApiScope("profile.graph", "Profile GraphQL"),
                new ApiScope("pushnotification.signalr", "Push Notification SignalR"),
            };
        }
        public static IEnumerable<Client> GetClients(IWebHostEnvironment env)
        {
            var devClients = new List<Client>
            {
                new Client
                {
                    ClientId = "adminapp.blazor",
                    ClientName = "Blazor Admin App",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = { "https://localhost:44343/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44343" },
                    AllowedCorsOrigins = { "https://localhost:44343" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        "pushnotification.signalr",
                        "event.graph",
                    }
                },
                new Client
                {
                    ClientId = "publicapp.blazor",
                    ClientName = "Blazor Public App",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = { "https://localhost:44338/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44338" },
                    AllowedCorsOrigins = { "https://localhost:44338" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        "pushnotification.signalr",
                        "event.graph",
                    }
                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = { "http://127.0.0.1:5500/callback.html" },
                    PostLogoutRedirectUris = { "http://127.0.0.1:5500/index.html" },
                    AllowedCorsOrigins = { "http://127.0.0.1:5500" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        "event.graph",
                        "profile.graph",
                        "pushnotification.signalr"
                    }
                },
            };

            var prodClients = new List<Client>
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = { "http://127.0.0.1:5500/callback.html" },
                    PostLogoutRedirectUris = { "http://127.0.0.1:5500/index.html" },
                    AllowedCorsOrigins = { "http://127.0.0.1:5500" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        "event.graph",
                        "profile.graph",
                        "pushnotification.signalr"
                    }
                },
            };

            return env.IsDevelopment() ? devClients : prodClients;
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
            };
        }
    }
}
