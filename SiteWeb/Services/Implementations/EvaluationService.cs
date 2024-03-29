﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using SiteWeb.Services.Interfaces;
using SiteWeb.Data;

namespace SiteWeb.Services.Implementations
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

        public async Task<ActionResult<List<EvaluationL>>> GetEvaluations()
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName));
            if (response.IsSuccessStatusCode)
            {
                var evaluations = await response.Content.ReadFromJsonAsync<List<EvaluationL>>();

                return evaluations;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<ActionResult<EvaluationL>> GetEvaluation(int id)
        {
            var response = await _client.GetAsync(string.Format("{0}/{1}/{2}", _configuration.ApiUrl, ControllerName, id));

            if (response.IsSuccessStatusCode)
            {
                var evaluation = await response.Content.ReadFromJsonAsync<EvaluationL>();

                return evaluation;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdateEvaluation(EvaluationL evaluation)
        {

            var response = await _client.PutAsJsonAsync(string.Format("{0}/{1}", _configuration.ApiUrl, ControllerName), evaluation);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> AddEvaluation(EvaluationL evaluation)
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
