using Frontend.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void StoreTokenAsCookie(string token)
    {
        var cookies = _httpContextAccessor.HttpContext?.Response.Cookies;

        cookies?.Append("auth_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(30)
        });
    }

    public void ClearToken()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("auth_token");
    }

    public bool IsTokenValid(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return jsonToken is not null && jsonToken.ValidTo > DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }
}
