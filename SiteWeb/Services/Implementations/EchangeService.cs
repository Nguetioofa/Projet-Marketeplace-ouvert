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

        public async Task<List<EchangeL>> GetEchanges()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var echanges = await response.Content.ReadFromJsonAsync<List<EchangeL>>();

                return echanges;

            }
            else
            {
                return new ();
            }

        }

        public async Task<EchangeL> GetEchange(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var echange = await response.Content.ReadFromJsonAsync<EchangeL>();

                return echange;

            }
            else
            {
                return new();
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

        public async Task<List<EchangeL>> GetEchangesByIdUser(int id)
        {
            var response = await _client.GetAsync($"{_configuration.ApiUrl}/{ControllerName}/GetEchangesByIdUser/{id}");

            if (response.IsSuccessStatusCode)
            {
                var echanges = await response.Content.ReadFromJsonAsync<List<EchangeL>>();

                return echanges;

            }
            else
            {
                return new();
            }
        }

        public async Task<bool> ChangeStatutTransaction(int idechange, int idstatut)
        {
            var response = await _client.PutAsync($"{_configuration.ApiUrl}/{ControllerName}/ChangeStatutTransaction/{idechange}/{idstatut}",null);
            return response.IsSuccessStatusCode;
        }
    }
}
