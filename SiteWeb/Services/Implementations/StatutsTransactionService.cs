using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
{
    public class StatutsTransactionService : IStatutsTransactionService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "StatutsTransactions";

        public StatutsTransactionService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<StatutsTransactionL>> GetStatutsTransactions()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                 var StatutsTransactions = await response.Content.ReadFromJsonAsync<List<StatutsTransactionL>>();

                return StatutsTransactions;

            }
            else
            {
                return new List<StatutsTransactionL>();
            }

        }

        public async Task<StatutsTransactionL> GetStatutsTransaction(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            if (response.IsSuccessStatusCode)
            {
                var StatutsTransaction = await response.Content.ReadFromJsonAsync<StatutsTransactionL>();
                return StatutsTransaction;
            }
            else
            {
                 return new StatutsTransactionL();
            }
        }

        public async Task<bool> UpdateStatutsTransaction(StatutsTransactionL StatutsTransaction)
        {
            var data = JsonSerializer.Serialize(StatutsTransaction);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddStatutsTransaction(StatutsTransactionL StatutsTransaction)
        {
            var data = JsonSerializer.Serialize(StatutsTransaction);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteStatutsTransaction(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
