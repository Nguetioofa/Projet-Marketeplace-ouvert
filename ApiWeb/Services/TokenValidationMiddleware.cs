using NuGet.Protocol;
using System.Security.Claims;

namespace ApiWeb.Services
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        public TokenValidationMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }
        public async Task Invoke(HttpContext context)
        {
            // Get the token from the authorization header
            var token = context.Request.ToJson();// .Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            // Validate the token and get the user id
            var userId = _tokenService.ValidateToken(token);
            if (userId != null)
            {
                // Create a claims principal with the user id as a claim
                var claims = new[] { new Claim(ClaimTypes.NameIdentifier,userId.ToString()) };
                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);
                // Assign the principal to the HTTP context
                context.User = principal;
                // Call the next middleware in the pipeline
                await _next(context);
            }
            else
            {
                // Return a 401 unauthorized response
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid or missing token");
            }
        }

    }
}
