using CSharpFunctionalExtensions;
using Frontend.ViewModel;

namespace Frontend.Services.Interface
{
    public interface IAuthService
    {
        Task<Result<string>> GetToken(LoginViewModel request);
    }
}
