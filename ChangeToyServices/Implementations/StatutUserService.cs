using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using ChangeToyServices.Interfaces;

namespace ChangeToyServices.Implementations
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

        public async Task<ActionResult<IEnumerable<StatutUserL>>> GetStatutUsers()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var userStatus = await response.Content.ReadFromJsonAsync<List<StatutUserL>>();

                // var content = await response.Content.ReadAsStringAsync();
                // var StatutUsers = JsonSerializer.Deserialize<List<StatutUser>>(content);

                return userStatus;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<StatutUserL>> GetStatutUser(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var StatutUser = await response.Content.ReadFromJsonAsync<StatutUserL>();
                //var StatutUser = JsonSerializer.Deserialize<StatutUser>(content);

                return StatutUser;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateStatutUser(StatutUserL StatutUser)
        {
            //var data = JsonSerializer.Serialize(StatutUser);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), StatutUser);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddStatutUser(StatutUserL StatutUser)
        {
            //var data = JsonSerializer.Serialize(StatutUser);
            //var content = new StringContent(Encoding.UTF8, "application/json");
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), StatutUser);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteStatutUser(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
