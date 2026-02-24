using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExampleCompanyApp.Api.AuthenticationService
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string role)
        {
            var jwt = _configuration.GetSection("Jwt"); // ayarlar appsettings.jsontutuluyor
            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role,role)

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(jwt["ExpireMinutes"])),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
