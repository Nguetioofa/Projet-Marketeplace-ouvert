using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using NuGet.Common;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

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
            //var token = User.FindFirst("Token")?.Value;

           // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjUiLCJOYW1lTGFzdE5hbWUiOiJzdHJpbmcgc3RyaW5nIiwiZW1haWwiOiJ1c2VyQGV4YW1wbGUuY29tIiwicm9sZSI6WyJ1dGlsaXNhdGV1ciIsIm1vZMOpcmF0ZXVyIiwiYWRtaW5pc3RyYXRldXIiLCJzdXBlci1hZG1pbmlzdHJhdGV1ciJdLCJuYmYiOjE2ODQ2NTMyNjEsImV4cCI6MTY4NTI1ODA2MSwiaWF0IjoxNjg0NjUzMjYxfQ.I-ZgR4-qYPwKM5OdZXRhYpfN3rp8zPSGwn7uL1h__7Q");
        }

        public async Task<IEnumerable<StatutUserL>> GetStatutUsers()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var userStatus = await response.Content.ReadFromJsonAsync<List<StatutUserL>>();
                return userStatus;
            }
            else
            {
                return null;
            }

        }

        public async Task<StatutUserL> GetStatutUser(int id)
        {
            //_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjUiLCJOYW1lTGFzdE5hbWUiOiJzdHJpbmcgc3RyaW5nIiwiZW1haWwiOiJ1c2VyQGV4YW1wbGUuY29tIiwicm9sZSI6WyJ1dGlsaXNhdGV1ciIsIm1vZMOpcmF0ZXVyIiwiYWRtaW5pc3RyYXRldXIiLCJzdXBlci1hZG1pbmlzdHJhdGV1ciJdLCJuYmYiOjE2ODQ2NTMyNjEsImV4cCI6MTY4NTI1ODA2MSwiaWF0IjoxNjg0NjUzMjYxfQ.I-ZgR4-qYPwKM5OdZXRhYpfN3rp8zPSGwn7uL1h__7Q");
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var StatutUser = await response.Content.ReadFromJsonAsync<StatutUserL>();
                return StatutUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateStatutUser(StatutUserL StatutUser)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), StatutUser);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddStatutUser(StatutUserL StatutUser)
        {
            
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
