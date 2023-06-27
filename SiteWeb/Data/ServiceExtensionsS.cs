using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SiteWeb.Services.Implementations;
using SiteWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.CodeAnalysis.Differencing;
using System.Net.Http.Headers;
using System.Net;

namespace SiteWeb.Data
{
    public static class ServiceExtensionsS
    {
        public static IServiceCollection AddChangeToyServiceS(this WebApplicationBuilder builder)
        {
            // Elle crée une instance de ProductService avec cette adresse et l'enregistre comme une implémentation de IProductService

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<AuthorizationHeaderHandler>();
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
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IModeLivraisonService, ModeLivraisonService>();
            builder.Services.AddScoped<IModePayementService, ModePayementService>();
            builder.Services.AddScoped<INewsletterService, NewsletterService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddSingleton<IStatutsTransactionService, StatutsTransactionService>();
            builder.Services.AddSingleton<IStatutUserService, StatutUserService>();
            builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

            return builder.Services;

        }
    }
}
