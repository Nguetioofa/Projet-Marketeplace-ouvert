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
            // Vérifiez si l'utilisateur est authentifié
            if (!context.User.Identity.IsAuthenticated && context.GetEndpoint()?.Metadata.GetMetadata<IAuthorizeData>() !=null/* && !context.Request.Path.StartsWithSegments("/Utilisateurs/Login")*/)
            {
                // Stockez l'URL de la ressource que l'utilisateur essayait d'accéder dans un cookie
                context.Response.Cookies.Append("ReturnUrl", context.Request.Path + context.Request.QueryString);

                // Redirigez l'utilisateur vers la page de connexion
                context.Response.Redirect("/Utilisateurs/Login");
                return;
            }

            await _next(context);
        }
    }
}
