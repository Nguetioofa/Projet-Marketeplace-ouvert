using Microsoft.AspNetCore.Mvc;
using SiteWeb.Services.Interfaces;
using System.Text.Json;
using System.Text;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class FonctionUserService : IFonctionUserService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "FonctionUsers";

        public FonctionUserService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<FonctionUser>>> GetFonctionUsers()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var FonctionUsers = JsonSerializer.Deserialize<List<FonctionUser>>(content);

                return FonctionUsers;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<FonctionUser>> GetFonctionUser(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var FonctionUser = JsonSerializer.Deserialize<FonctionUser>(content);

                return FonctionUser;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateFonctionUser(FonctionUser FonctionUser)
        {
            var data = JsonSerializer.Serialize(FonctionUser);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddFonctionUser(FonctionUser FonctionUser)
        {
            var data = JsonSerializer.Serialize(FonctionUser);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteFonctionUser(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
