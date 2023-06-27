using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class JouetService : IJouetService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Jouets";

        public JouetService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<Jouet>> GetJouets()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var Jouets = await response.Content.ReadFromJsonAsync<List<Jouet>>();

                return Jouets;

            }
            else
            {
                return null;
            }

        }

        public async Task<Jouet> GetJouet(int id)
        {
            var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jouet = await response.Content.ReadFromJsonAsync<Jouet>();
                
                return jouet;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateJouet(Jouet Jouet)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Jouet);

            return response.IsSuccessStatusCode;

        }
        public async Task<Jouet> AddJouet(Jouet Jouet)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), Jouet);

            if (response.IsSuccessStatusCode)
            {
               Jouet? jouet = await response.Content.ReadFromJsonAsync<Jouet>(); 
                return jouet;
            }
            return null;
            
        }
        public async Task<bool> DeleteJouet(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
