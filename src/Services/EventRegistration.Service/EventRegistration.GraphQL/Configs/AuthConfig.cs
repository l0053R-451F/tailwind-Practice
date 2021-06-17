﻿using EventRegistration.GraphQL.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventRegistration.GraphQL.Configs
{
    public static class AuthConfig
    {
        public static void AddIdentityServerAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration
                .GetSection("AppSettings").Get<AppSettings>();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = settings.IdentityServerConfig.Issuer;
                options.ApiName = settings.IdentityServerConfig.Audience;
            });
            services.AddAuthorization();
        }
    }

}
