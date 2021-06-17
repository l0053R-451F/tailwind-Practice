using Event.Application.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Event.Infrastructure.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
            {
                var user = _httpContextAccessor.HttpContext.User;

                this.UserId = user.Claims.First(x => x.Type == JwtClaimTypes.Subject).Value;
                //this.UserEmail = user.Claims.First(x => x.Type == JwtClaimTypes.Email).Value;
                //this.PhoneNumber = user.Claims.First(x => x.Type == JwtClaimTypes.PhoneNumber).Value;
                //this.UserName = user.Claims.First(x => x.Type == JwtClaimTypes.PreferredUserName).Value;
                this.IsAuthenticated = user.Identity.IsAuthenticated;
                this.Roles = user.Claims.Where(c => c.Type == JwtClaimTypes.Role).Select(c => c.Value).ToList();
            }
        }

        public string UserId { get; }

        public string UserEmail { get; }

        public string PhoneNumber { get; }

        public string UserName { get; }

        public string RequestOrigin { get; }

        public List<string> Roles { get; }

        public bool IsAuthenticated { get; }
    }
}
