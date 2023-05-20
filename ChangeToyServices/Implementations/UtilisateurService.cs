using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using ChangeToyServices.Interfaces;
using System.Net.Http.Json;
using ModelsLibrary.Models.Users;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;


namespace ChangeToyServices.Implementations
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Utilisateurs";

        public UtilisateurService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<UtilisateurL>>> GetUtilisateurs()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var Utilisateurs = await response.Content.ReadFromJsonAsync<List<UtilisateurL>>();

                return Utilisateurs;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<UtilisateurL>> GetUtilisateur(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var Utilisateur = await response.Content.ReadFromJsonAsync<UtilisateurL>();

                return Utilisateur;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateUtilisateur(UtilisateurL Utilisateur)
        {
         
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Utilisateur);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddUtilisateur(UtilisateurL Utilisateur)
        {

            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Utilisateur);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteUtilisateur(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

        public async Task<(ClaimsPrincipal principal, string errorMessage)> Login(UserAuthen model)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName,"Login"), model);

            if (response.IsSuccessStatusCode)
            {

                var useraut = await response.Content.ReadFromJsonAsync<UserTokensDto>();
                var key = Encoding.UTF8.GetBytes(_configuration.IssuerSigningKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal2 = new ClaimsPrincipal();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = _configuration.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = _configuration.ValidateIssuer,
                    ValidateAudience = _configuration.ValidateAudience
                };
                try
                {
                    var principal = tokenHandler.ValidateToken(useraut.Token, validationParameters, out var validatedToken);
                    var roleClaims = principal.FindAll(ClaimTypes.Role);
                    var roles = roleClaims.Select(claim => claim.Value).ToList();

                    // Étape 4: Créer des revendications pour les informations de connexion et les rôles
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name, useraut.UserName),
                    //    new Claim(ClaimTypes.Email, useraut.EmailId)
                    //    // Ajouter d'autres revendications ici
                    //};
                    var jwtToken = tokenHandler.ReadJwtToken(useraut.Token);

                    // Get the claims from the token
                    var claims = jwtToken.Claims;

                    // Get the role claims
                    var roleClaimcs = claims.Where(c => c.Type == ClaimTypes.Role);

                    // Get the list of roles
                    var rolces = roleClaimcs.Select(c => c.Value).ToList();
                    //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    // Étape 5: Créer une nouvelle instance de ClaimsIdentity
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                     principal2 = new ClaimsPrincipal(identity);

                    // Étape 6: Signer l'utilisateur en utilisant l'instance de ClaimsPrincipal
                   // await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal2);
                }
                catch (Exception ex)
                {
                    // Gérer l'erreur de validation ici
                }
                return (principal2, null);
            }
            else
            {

                var error = await response.Content.ReadAsStringAsync();
                return (null, error);
            }

        }

        public async Task<object> Register(UserResisterDto userResisterDto)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), userResisterDto);

            if (response.IsSuccessStatusCode)
            {
                var useraut = await response.Content.ReadFromJsonAsync<UserTokensDto>();
                return useraut;
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<MessageErrorG>();
                return error;
            }
        }
    }
}
