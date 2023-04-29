using Microsoft.AspNetCore.Mvc;
using ChangeToyServices.Interfaces;
using System.Net.Http.Json;


namespace ChangeToyServices.Implementations
{
    public class EvaluationService : IEvaluationService
    {
        private readonly HttpClient _client;
        private readonly IConfigurationService _configuration;
        private readonly string ControllerName = "Evaluations";

        public EvaluationService(IConfigurationService configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<ActionResult<List<Evaluation>>> GetEvaluations()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var evaluations = await response.Content.ReadFromJsonAsync<List<Evaluation>>();

                return evaluations;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<Evaluation>> GetEvaluation(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var evaluation = await response.Content.ReadFromJsonAsync<Evaluation>();

                return evaluation;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateEvaluation(Evaluation evaluation)
        {
 
            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), evaluation);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddEvaluation(Evaluation evaluation)
        {

            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), evaluation);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> DeleteEvaluation(int id)
        {
            var response = await _client.DeleteAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));
            return response.IsSuccessStatusCode;

        }
    }
}
