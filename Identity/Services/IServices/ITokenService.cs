using Identity.Domain;
using Identity.Dtos;

namespace Identity.Services.IServices
{
    public interface ITokenService
    {
        Task<AuthToken> GenerateJwtToken(LoginModel model);
    }
}
