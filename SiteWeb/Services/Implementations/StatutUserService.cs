using Microsoft.AspNetCore.Mvc;
using SiteWeb.Data;
using SiteWeb.Services.Interfaces;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace SiteWeb.Services.Implementations
{
    public class StatutUserService : IStatutUserService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "StatutUsers";

        public StatutUserService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<IEnumerable<StatutUser>>> GetStatutUsers()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var userStatus = await response.Content.ReadFromJsonAsync<List<StatutUser>>();

               // var content = await response.Content.ReadAsStringAsync();
               // var StatutUsers = JsonSerializer.Deserialize<List<StatutUser>>(content);

                return userStatus;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<StatutUser>> GetStatutUser(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var StatutUser = await response.Content.ReadFromJsonAsync<StatutUser>();
                //var StatutUser = JsonSerializer.Deserialize<StatutUser>(content);

                return StatutUser;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateStatutUser(StatutUser StatutUser)
        {
            //var data = JsonSerializer.Serialize(StatutUser);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsJsonAsync<StatutUser>(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName),StatutUser);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddStatutUser(StatutUser StatutUser)
        {
            //var data = JsonSerializer.Serialize(StatutUser);
            //var content = new StringContent(Encoding.UTF8, "application/json");
            var response = await _client.PostAsJsonAsync<StatutUser>(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), StatutUser);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteStatutUser(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
