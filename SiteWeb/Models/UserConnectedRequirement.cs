using Microsoft.AspNetCore.Authorization;

namespace SiteWeb.Models
{

	public class UserConnectedRequirement : IAuthorizationRequirement
	{
		public int UserId { get; set; }

		public UserConnectedRequirement() : base()
		{
		}

	}
}
