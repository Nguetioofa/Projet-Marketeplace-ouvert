namespace SiteWeb.Routes
{
    public static class RouteConfig
    {
        public static void ConfigureRoutes(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            
            //app.MapControllerRoute(
            //name: "StatutUser",
            //pattern: "StatutUser/{id}",
            //defaults: new { controller = "StatutUser", action = "Index/{id}" });

            app.MapControllerRoute(
            name: "StatutUser",
            pattern: "{controller=StatutUser}/{action=Index}/{id?}");

        }


    }
}
