using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class ModePayementService : IModePayementService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "ModePayements";

        public ModePayementService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<ModePayementL>> GetModePayements()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {

                List<ModePayementL>? ModePayements = await response.Content.ReadFromJsonAsync<List<ModePayementL>>();

                return ModePayements;

            }
            else
            {
                return new List<ModePayementL>();
            }

        }

        public async Task<ModePayementL> GetModePayement(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var ModePayement = await response.Content.ReadFromJsonAsync<ModePayementL>();
                return ModePayement;
            }
            else
            {
                return new ModePayementL();
            }
        }

        public async Task<bool> UpdateModePayement(ModePayementL ModePayement)
        {
            var data = JsonSerializer.Serialize(ModePayement);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddModePayement(ModePayementL ModePayement)
        {
            var data = JsonSerializer.Serialize(ModePayement);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteModePayement(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
