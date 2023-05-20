using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMyServices(this WebApplicationBuilder builder)
        {
            //builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            //builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            //builder.Services.AddHttpClient();

            // Get the token value
            builder.AddChangeToyServiceS();
           
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
                //.AddCookie(options =>
                //{
                //    options.LoginPath = "/Utilisateurs/Login";
                //    //options.LogoutPath = "/";
                //});

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministrateurSeulement", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "administrateur"));
            });

            return builder.Services;


            /*Pour changer la durer de de vie de HttpMessageHandler (on le fait pour des raisons de securite
              si non c'est mieux de laisser par defaut qui est Singleton donc une seul instance)
                services.AddHttpClient<IMonService, MonService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
             */
        }
    }
}
