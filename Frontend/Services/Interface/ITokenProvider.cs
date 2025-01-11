namespace Frontend.Services.Interface
{
    public interface ITokenProvider
    {
        void StoreTokenAsCookie(string token);
        bool IsTokenValid(string token);
    }
}
