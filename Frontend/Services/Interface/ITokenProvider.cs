namespace Frontend.Services.Interface
{
    public interface ITokenProvider
    {
        void StoreTokenAsCookie(string token);
        void ClearToken();
        bool IsTokenValid(string token);
    }
}
