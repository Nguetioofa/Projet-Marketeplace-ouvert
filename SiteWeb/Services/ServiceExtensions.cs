using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using Microsoft.AspNetCore.Authorization;
using SiteWeb.Services.CustomAuthorization;
using SiteWeb.Models;

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
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IAuthorizationHandler, UserConnectedAuthorizationHandler>();
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
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IModeLivraisonService, ModeLivraisonService>();
            builder.Services.AddScoped<IModePayementService, ModePayementService>();
            builder.Services.AddScoped<INewsletterService, NewsletterService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddSingleton<IStatutsTransactionService, StatutsTransactionService>();
            builder.Services.AddSingleton<IStatutUserService, StatutUserService>();
            builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/Error403";
                    options.LoginPath = "/Utilisateurs/Login";
				});

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministrateurSeulement", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "administrateur"));
				options.AddPolicy("ProfileAccessPolicy", policy =>
			        policy.Requirements.Add(new UserConnectedRequirement()));

                    options.AddPolicy("PropiertaireJouetOuAdminPolicy", policy =>
                    {
                        policy.Requirements.Add(new OwnerRequirement());
                    });
               

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
