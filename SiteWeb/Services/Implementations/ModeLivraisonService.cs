using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using ModelsLibrary.Models.Toys;

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

        public async Task<List<ModeLivraisonL>> GetModeLivraisons()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var ModeLivraisons = await response.Content.ReadFromJsonAsync<List<ModeLivraisonL>>();

                return ModeLivraisons;

            }
            else
            {
                return new List<ModeLivraisonL>();
            }

        }

        public async Task<ModeLivraisonL> GetModeLivraison(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var ModeLivraison = await response.Content.ReadFromJsonAsync<ModeLivraisonL>();

                return ModeLivraison;
            }
            else
            {
                return new ModeLivraisonL();
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
