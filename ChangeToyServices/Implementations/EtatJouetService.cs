using Microsoft.AspNetCore.Mvc;
using ChangeToyServices.Interfaces;
using System.Net.Http.Json;


namespace ChangeToyServices.Implementations
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

        public async Task<ActionResult<List<EtatJouet>>> GetEtatJouets()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var EtatJouets = await response.Content.ReadFromJsonAsync<List<EtatJouet>>();

                return EtatJouets;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<EtatJouet>> GetEtatJouet(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var etatJouet = await response.Content.ReadFromJsonAsync<EtatJouet>();

                return etatJouet;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateEtatJouet(EtatJouet etatJouet)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), etatJouet);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddEtatJouet(EtatJouet etatJouet)
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
