using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class EtatJouetService : IEtatJouetService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "EtatJouets";

        public EtatJouetService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<EtatJouetL>> GetEtatJouets()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var EtatJouets = await response.Content.ReadFromJsonAsync<List<EtatJouetL>>();

                return EtatJouets;

            }
            else
            {
                return new List<EtatJouetL>();
            }

        }

        public async Task<EtatJouetL> GetEtatJouet(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var etatJouet = await response.Content.ReadFromJsonAsync<EtatJouetL>();

                return etatJouet;

            }
            else
            {
                return new EtatJouetL();
            }
        }

        public async Task<bool> UpdateEtatJouet(EtatJouetL etatJouet)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), etatJouet);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddEtatJouet(EtatJouetL etatJouet)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), etatJouet);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteEtatJouet(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
