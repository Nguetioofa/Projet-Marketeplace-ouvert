global using SiteWeb.Data;
global using ModelsLibrary.Models;
using SiteWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using System.Net.Http.Json;

namespace SiteWeb.Services.Implementations
{
    public class AbonnementService : IAbonnementService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Abonnements";
        public AbonnementService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<AbonnementL>>> GetAbonnements()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var abonnements = await response.Content.ReadFromJsonAsync<List<AbonnementL>>();

                return abonnements;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<AbonnementL>> GetAbonnement(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var abonnement = await response.Content.ReadFromJsonAsync<AbonnementL>();

                return abonnement;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateAbonnement(AbonnementL abonnement)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), abonnement);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddAbonnement(AbonnementL abonnement)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), abonnement);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteAbonnement(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

    }
}
