using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeToyServices.Data
{
    public static class ServiceExtensionsS
    {
        public static IServiceCollection AddChangeToyServiceS(this WebApplicationBuilder builder)
        {
            // Elle crée une instance de ProductService avec cette adresse et l'enregistre comme une implémentation de IProductService

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            builder.Services.AddHttpClient();
           // builder.Services.AddSingleton<HttpContext, HttpContext>();

            return builder.Services;

        }
    }
}
