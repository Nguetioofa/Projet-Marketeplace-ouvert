using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using ChangeToyServices.Interfaces;
using System.Net.Http.Json;


namespace ChangeToyServices.Implementations
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

        public async Task<ActionResult<List<Annonce>>> GetAnnonces()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var annonces = await response.Content.ReadFromJsonAsync<List<Annonce>>();

                return annonces;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<Annonce>> GetAnnonce(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var annonce = await response.Content.ReadFromJsonAsync<Annonce>();

                return annonce;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateAnnonce(Annonce annonce)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), annonce);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddAnnonce(Annonce annonce)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), annonce);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteAnnonce(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
