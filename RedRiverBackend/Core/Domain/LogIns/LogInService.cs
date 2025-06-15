using System.Security.Principal;

using RedRiverApp.Core.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace RedRiverApp.Core.Domain.LogIns
{
    public class LogInService
    {
        private readonly UserService userService;

        public LogInService(UserService userService)
        {
            this.userService = userService;
        }

        public string? IsInloggad(LogIn logIn)
        {
            bool isValid = userService.LogIn(logIn);

            if (!isValid)
            {
                return null;
            }

            // Generate a token (simple JWT example)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this_is_a_very_secret_key_12345678!"); // Replace with real secret in appsettings

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, logIn.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}