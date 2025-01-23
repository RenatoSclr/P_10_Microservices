using Identity.Domain;
using Identity.Domain.Dtos;
using Identity.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public TokenService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthToken> GenerateJwtToken(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password)) 
                return new AuthToken { Token = string.Empty, ExpirationTime = 0 };

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]));
            var expirationTimeStamp = DateTime.Now.AddHours(8);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                expires: expirationTimeStamp,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var expire = (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds;

            return new AuthToken{Token = tokenString,ExpirationTime = expire};
        }
    }   
}
