using Microsoft.Extensions.Options;

namespace ChangeToyServices.Data
{
    public class ConfigurationService : IConfigurationService
    {
        public string ApiUrl { get; }
        public bool ValidateIssuerSigningKey { get; }
        public string IssuerSigningKey { get; }
        public bool ValidateAudience { get; }
        public bool ValidateIssuer { get; }

        public ConfigurationService(IOptions<ApiSettings> apiSettings)
        {
            ApiUrl = apiSettings.Value.ApiUrl;
            ValidateIssuerSigningKey = apiSettings.Value.ValidateIssuerSigningKey;
            IssuerSigningKey = apiSettings.Value.IssuerSigningKey;
            ValidateAudience = apiSettings.Value.ValidateAudience;
            ValidateIssuer = apiSettings.Value.ValidateIssuer;
        }
    }
}
