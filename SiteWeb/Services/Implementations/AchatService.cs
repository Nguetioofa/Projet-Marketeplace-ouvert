using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using ModelsLibrary.Models.Achats;

namespace SiteWeb.Services.Implementations
{
    public class AchatService : IAchatService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Achats";

        public AchatService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<AchatL>>> GetAchats()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var achats = await response.Content.ReadFromJsonAsync<List<AchatL>>();

                return achats;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<AchatL>> GetAchat(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var achat = await response.Content.ReadFromJsonAsync<AchatL>();

                return achat;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateAchat(AchatL achat)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), achat);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddAchat(AchatL achat)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), achat);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteAchat(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }

    }
}
