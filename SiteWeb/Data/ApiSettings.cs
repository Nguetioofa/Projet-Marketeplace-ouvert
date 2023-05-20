namespace SiteWeb.Data
{
    public class ApiSettings
    {
        public string ApiUrl { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }

    }
}
