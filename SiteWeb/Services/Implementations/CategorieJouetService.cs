using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class CategorieJouetService : ICategorieJouetService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "CategorieJouets";

        public CategorieJouetService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<CategorieJouetL>> GetCategorieJouets()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var categorieJouets = await response.Content.ReadFromJsonAsync<List<CategorieJouetL>>();
                return categorieJouets;
            }
            else
            {
                return new List<CategorieJouetL>();
            }

        }

        public async Task<CategorieJouetL> GetCategorieJouet(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var categorieJouet = await response.Content.ReadFromJsonAsync<CategorieJouetL>();

                return categorieJouet;

            }
            else
            {
                return new CategorieJouetL();
            }
        }

        public async Task<bool> UpdateCategorieJouet(CategorieJouetL categorieJouet)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), categorieJouet);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddCategorieJouet(CategorieJouetL categorieJouet)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), categorieJouet);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteCategorieJouet(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;
        }
    }
}
