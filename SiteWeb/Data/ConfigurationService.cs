using Microsoft.Extensions.Options;
using NuGet.Configuration;

namespace SiteWeb.Data
{
    public class ConfigurationService : IConfigurationService
    {
        public string ApiUrl { get; }

        public ConfigurationService(IOptions<ApiSettings> apiSettings)
        {
            ApiUrl = apiSettings.Value.ApiUrl;
        }
    }
}
