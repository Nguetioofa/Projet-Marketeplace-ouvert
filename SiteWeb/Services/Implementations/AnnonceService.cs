using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using ModelsLibrary.Models.Annonces;

namespace SiteWeb.Services.Implementations
{
    public class AnnonceService : IAnnonceService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Annonces";

        public AnnonceService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<AnnonceL>> GetAnnonces()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var annonces = await response.Content.ReadFromJsonAsync<List<AnnonceL>>();

                return annonces;

            }
            else
            {
				return null;
			}

		}

        public async Task<AnnonceL> GetAnnonce(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var annonce = await response.Content.ReadFromJsonAsync<AnnonceL>();

                return annonce;

            }
            else
            {
                return null;
                    //throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateAnnonce(AnnonceL annonce)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), annonce);
            return response.IsSuccessStatusCode;

        }
        public async Task<AnnonceL> AddAnnonce(AnnonceL annonce)
        {
			var response = await _client.PostAsJsonAsync($"{_configuration.ApiUrl}/{ControllerName}",annonce);

			if (response.IsSuccessStatusCode)
			{
				var annonce1 = await response.Content.ReadFromJsonAsync<AnnonceL>();
				return annonce1;
			}
			return null;

        }
        public async Task<bool> DeleteAnnonce(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
