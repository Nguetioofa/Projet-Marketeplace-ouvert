using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;
using ModelsLibrary.Models.Echanges;

namespace SiteWeb.Services.Implementations
{
    public class EchangeService : IEchangeService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Echanges";

        public EchangeService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<EchangeL>>> GetEchanges()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var echanges = await response.Content.ReadFromJsonAsync<List<EchangeL>>();

                return echanges;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<EchangeL>> GetEchange(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var echange = await response.Content.ReadFromJsonAsync<EchangeL>();

                return echange;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateEchange(EchangeL echange)
        {
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), echange);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddEchange(EchangeL echange)
        {
            var response = await _client.PostAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), echange);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteEchange(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
