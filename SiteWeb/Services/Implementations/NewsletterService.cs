using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class NewsletterService : INewsletterService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Newsletters";

        public NewsletterService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<NewsletterL>>> GetNewsletters()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Newsletters = JsonSerializer.Deserialize<List<NewsletterL>>(content);

                return Newsletters;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<NewsletterL>> GetNewsletter(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Newsletter = JsonSerializer.Deserialize<NewsletterL>(content);

                return Newsletter;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateNewsletter(NewsletterL Newsletter)
        {
            var data = JsonSerializer.Serialize(Newsletter);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddNewsletter(NewsletterL Newsletter)
        {
            var data = JsonSerializer.Serialize(Newsletter);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteNewsletter(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
