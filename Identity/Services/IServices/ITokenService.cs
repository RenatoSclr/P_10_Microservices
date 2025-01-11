using Identity.Domain;
using Identity.Domain.Dtos;

namespace Identity.Services.IServices
{
    public interface ITokenService
    {
        Task<AuthToken> GenerateJwtToken(LoginModel model);
    }
}
