using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

namespace SiteWeb.Services.CustomAuthorization
{
    public static class RazorPageExtensions
    {
        public static bool IsAuthorized(this RazorPageBase page, string ownerId)
        {
            if (page.User.IsInRole("Admin"))
            {
                return true;
            }

            var userId = page.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ownerId == userId)
            {
                return true;
            }

            return false;
        }

        /*@if (this.IsAuthorized(Model.OwnerId))
{
    <div>Composant à afficher</div>
}
*/
    }

}
