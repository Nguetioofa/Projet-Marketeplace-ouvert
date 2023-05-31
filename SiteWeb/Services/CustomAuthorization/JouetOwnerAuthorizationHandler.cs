using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SiteWeb.Services.CustomAuthorization
{
    public class JouetOwnerAuthorizationHandler : AuthorizationHandler<OwnerRequirement, Jouet>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JouetOwnerAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement, Jouet jouet)
        {
            if (context.User == null || jouet == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole("administrateur"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            int idUser = Convert.ToInt32(context.User.FindFirst(ClaimTypes.Name)?.Value);
            if (jouet.Proprietaire == idUser)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }

    public class OwnerRequirement : IAuthorizationRequirement { }
}
