using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using ChangeToyServices.Interfaces;

namespace ChangeToyServices.Implementations
{
    public class JouetsPhotoService : IJouetsPhotoService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "JouetsPhotos";

        public JouetsPhotoService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<JouetsPhoto>>> GetJouetsPhotos()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var JouetsPhotos = JsonSerializer.Deserialize<List<JouetsPhoto>>(content);

                return JouetsPhotos;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<JouetsPhoto>> GetJouetsPhoto(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var JouetsPhoto = JsonSerializer.Deserialize<JouetsPhoto>(content);

                return JouetsPhoto;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateJouetsPhoto(JouetsPhoto JouetsPhoto)
        {
            var data = JsonSerializer.Serialize(JouetsPhoto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddJouetsPhoto(JouetsPhoto JouetsPhoto)
        {
            var data = JsonSerializer.Serialize(JouetsPhoto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteJouetsPhoto(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
