using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class ModeLivraisonService : IModeLivraisonService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "ModeLivraisons";

        public ModeLivraisonService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<ModeLivraisonL>>> GetModeLivraisons()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ModeLivraisons = JsonSerializer.Deserialize<List<ModeLivraisonL>>(content);

                return ModeLivraisons;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<ModeLivraisonL>> GetModeLivraison(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ModeLivraison = JsonSerializer.Deserialize<ModeLivraisonL>(content);

                return ModeLivraison;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateModeLivraison(ModeLivraisonL ModeLivraison)
        {
            var data = JsonSerializer.Serialize(ModeLivraison);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddModeLivraison(ModeLivraisonL ModeLivraison)
        {
            var data = JsonSerializer.Serialize(ModeLivraison);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteModeLivraison(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
