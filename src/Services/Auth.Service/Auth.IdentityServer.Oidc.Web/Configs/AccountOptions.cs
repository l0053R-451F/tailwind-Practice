using System;

namespace Auth.IdentityServer.Oidc.Web.Configs
{
    public class AccountOptions
    {
        public const bool AllowLocalLogin = true;
        public const bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public const bool ShowLogoutPrompt = true;
        public const bool AutomaticRedirectAfterSignOut = false;

        public const string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
