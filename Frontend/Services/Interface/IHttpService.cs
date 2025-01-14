using CSharpFunctionalExtensions;

namespace Frontend.Services.Interface
{
    public interface IHttpService
    {
        Task<Result<T>> GetAsync<T>(string url, string token);
        Task<Result> PostAsync<T>(string url, T data, string token);
        Task<Result> PutAsync<T>(string url, T data, string token);
        Task<Result> DeleteAsync(string url, string token);
    }
}
