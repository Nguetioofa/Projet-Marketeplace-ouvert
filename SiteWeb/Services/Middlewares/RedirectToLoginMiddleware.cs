using Microsoft.AspNetCore.Authorization;

namespace SiteWeb.Services.Middlewares
{
    public class RedirectToLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated && context.GetEndpoint()?.Metadata.GetMetadata<IAuthorizeData>() !=null/* && !context.Request.Path.StartsWithSegments("/Utilisateurs/Login")*/)
            {
                context.Response.Cookies.Append("ReturnUrl", context.Request.Path + context.Request.QueryString);

                context.Response.Redirect("/Utilisateurs/Login");
                return;
            }

            await _next(context);
        }
    }
}
