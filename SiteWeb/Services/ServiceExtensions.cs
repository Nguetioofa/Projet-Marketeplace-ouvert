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
            //builder.AddChangeToyServiceS();

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            //builder.Services.AddHttpContextAccessor();
            //builder.Services.AddTransient<AuthorizationHeaderHandler>();
            builder.Services.AddHttpClient();
            //builder.Services.AddHttpClient().AddHttpContextAccessor();// (() => new AuthorizationHeaderHandler());/*.AddHttpMessageHandler<AuthorizationHeaderHandler>();*/
            builder.Services.AddScoped<IAbonnementService, AbonnementService>();
            builder.Services.AddScoped<IAchatService, AchatService>();
            builder.Services.AddScoped<IAnnonceService, AnnonceService>();
            builder.Services.AddSingleton<ICategorieJouetService, CategorieJouetService>();
            builder.Services.AddScoped<ICommentaireService, CommentaireService>();
            builder.Services.AddScoped<IEchangeService, EchangeService>();
            builder.Services.AddSingleton<IEtatJouetService, EtatJouetService>();
            builder.Services.AddScoped<IEvaluationService, EvaluationService>();
            builder.Services.AddScoped<IFonctionUserService, FonctionUserService>();
            builder.Services.AddScoped<IJouetService, JouetService>();
            builder.Services.AddScoped<IJouetsPhotoService, JouetsPhotoService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IMessagesPhotoService, MessagesPhotoService>();
            builder.Services.AddScoped<IModeLivraisonService, ModeLivraisonService>();
            builder.Services.AddScoped<IModePayementService, ModePayementService>();
            builder.Services.AddScoped<INewsletterService, NewsletterService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddSingleton<IStatutsTransactionService, StatutsTransactionService>();
            builder.Services.AddSingleton<IStatutUserService, StatutUserService>();
            builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
            builder.Services.AddScoped<IUtilisateursProfilService, UtilisateursProfilService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/Error403";
                    options.LoginPath = "/Utilisateurs/Login";
                    //options.LogoutPath = "/Utilisateurs/Logout";
                });
            ////.AddCookie(options =>
            ////{
            ////    options.LoginPath = "/Utilisateurs/Login";
            ////    //options.LogoutPath = "/";
            ////});
           // builder.Services.AddAuthorizationBuilder();
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
