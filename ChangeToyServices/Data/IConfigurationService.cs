namespace ChangeToyServices.Data
{
    public interface IConfigurationService
    {
        string ApiUrl { get;}
        public bool ValidateIssuerSigningKey { get; }
        public string IssuerSigningKey { get; }
        public bool ValidateAudience { get; }
        public bool ValidateIssuer { get; }
    }
}
