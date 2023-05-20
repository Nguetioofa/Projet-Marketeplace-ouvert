using ChangeToyServices.Interfaces;
using ChangeToyServices.Implementations;
using System.Configuration;
using ChangeToyServices.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SiteWeb.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMyServices(this WebApplicationBuilder builder)
        {
            //builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            //builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            //builder.Services.AddHttpClient();
            builder.AddChangeToyServiceS();
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
                .AddCookie();
            return builder.Services;

            /*Pour changer la durer de de vie de HttpMessageHandler (on le fait pour des raisons de securite
              si non c'est mieux de laisser par defaut qui est Singleton donc une seul instance)
                services.AddHttpClient<IMonService, MonService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
             */
        }
    }
}
