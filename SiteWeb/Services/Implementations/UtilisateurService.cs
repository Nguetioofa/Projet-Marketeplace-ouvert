using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using ModelsLibrary.Models.Users;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
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

        public async Task<(UserTokensDto useraut, string errorMessage)> Login(UserAuthen model)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, "Login"), model);

            if (response.IsSuccessStatusCode)
            {

                var useraut = await response.Content.ReadFromJsonAsync<UserTokensDto>();



                return (useraut, null);
            }
            else
            {

                var error = await response.Content.ReadAsStringAsync();
                return (null, error);
            }

        }

        public async Task<(bool iSucess, string message)> Register(UserResisterDto userResisterDto)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, "Register"), userResisterDto);

            if (response.IsSuccessStatusCode)
            {
                var useregist = await response.Content.ReadAsStringAsync();
                return (true,useregist);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
        }
    }
}
