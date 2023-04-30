using ApiWeb.ModelDto;
using ApiWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ApiWeb.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _appSettings;
        public TokenService(IOptions<JwtSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserTokens GenerateToken(UserDto user, List<Role> roles)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.IssuerSigningKey);

            // create a list of claims with user id, email and roles
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Nom)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

           
            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);
            // DefaultHttpContext.(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //    ExpiresUtc = DateTime.UtcNow.AddDays(7)
            //});

            return new UserTokens
            {
                Id = user.Id,
                UserName = user.UserName,
                EmailId = user.Email,
                Token = tokenHandler.WriteToken(token),
                Validaty = TimeSpan.FromDays(7),
                ExpiredTime = DateTime.UtcNow.AddDays(7),
                GuidId = Guid.NewGuid()
            };
        }
        public int? ValidateToken(string token)
        {
            if (token == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.IssuerSigningKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time(instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    var idValue = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                    if (int.TryParse(idValue, out var userId))
                    {
                        // return user id from JWT token if validation successful
                        return userId;
                    }
                }
                // return null if validation fails
                return null;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }

}
