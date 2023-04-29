using Microsoft.AspNetCore.Mvc;
using SiteWeb.Data;
using SiteWeb.Services.Interfaces;
using System.Text.Json;
using System.Text;

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

        public async Task<ActionResult<List<Utilisateur>>> GetUtilisateurs()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var Utilisateurs = await response.Content.ReadFromJsonAsync<List<Utilisateur>>();
               // var Utilisateurs = JsonSerializer.Deserialize<List<Utilisateur>>(content);

                return Utilisateurs;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var Utilisateur = await response.Content.ReadFromJsonAsync<Utilisateur>();
                //var Utilisateur = JsonSerializer.Deserialize<Utilisateur>(content);

                return Utilisateur;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateUtilisateur(Utilisateur Utilisateur)
        {
            //var data = JsonSerializer.Serialize(Utilisateur);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Utilisateur);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddUtilisateur(Utilisateur Utilisateur)
        {
            //var data = JsonSerializer.Serialize(Utilisateur);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Utilisateur);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteUtilisateur(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
