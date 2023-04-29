using Microsoft.Extensions.Options;

namespace ChangeToyServices.Data
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
