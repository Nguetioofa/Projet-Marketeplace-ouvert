using Microsoft.AspNetCore.Authorization;
using SiteWeb.Models;
using System.Security.Claims;

namespace SiteWeb.Services.CustomAuthorization
{
	public class UserConnectedAuthorizationHandler : AuthorizationHandler<UserConnectedRequirement>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserConnectedAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserConnectedRequirement requirement)
		{
			if (!context.User.Identity.IsAuthenticated)
			{
				return Task.CompletedTask;
			}
			var routeValues = _httpContextAccessor.HttpContext?.GetRouteData().Values;
			if (routeValues.TryGetValue("id", out var id))
			{
				requirement.UserId = Convert.ToInt32(id);
			}
			if (context.User.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == requirement.UserId.ToString()))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
