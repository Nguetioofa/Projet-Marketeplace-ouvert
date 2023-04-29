using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using ChangeToyServices.Interfaces;

namespace ChangeToyServices.Implementations
{
    public class UtilisateursProfilService : IUtilisateursProfilService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "UtilisateursProfils";

        public UtilisateursProfilService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<UtilisateursProfil>>> GetUtilisateursProfils()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var UtilisateursProfils = JsonSerializer.Deserialize<List<UtilisateursProfil>>(content);

                return UtilisateursProfils;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<UtilisateursProfil>> GetUtilisateursProfil(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var UtilisateursProfil = JsonSerializer.Deserialize<UtilisateursProfil>(content);

                return UtilisateursProfil;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateUtilisateursProfil(UtilisateursProfil UtilisateursProfil)
        {
            var data = JsonSerializer.Serialize(UtilisateursProfil);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddUtilisateursProfil(UtilisateursProfil UtilisateursProfil)
        {
            var data = JsonSerializer.Serialize(UtilisateursProfil);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteUtilisateursProfil(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
