using System.Net.Http;

namespace SiteWeb.Data
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _client;

        public TokenMiddleware(RequestDelegate next, HttpClient client)
        {
            _next = next;
            _client = client;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Récupérer le jeton du claimprincipal
            var token = context.User.FindFirst("Token")?.Value;
            //// Ajouter le jeton à l'en-tête d'autorisation de la requête HTTP
            //if (!string.IsNullOrEmpty(token))
            //{
            //    context.Request.Headers.Add("Authorization", "Bearer " + token);
            //}
            // Ajouter le jeton à l'en-tête d'autorisation
            if (!string.IsNullOrEmpty(token))
            {
                //context.Request.Headers.Add("Authorization", "Bearer" + token)httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Appeler le middleware suivant
            await _next(context);
        }
    }
}
