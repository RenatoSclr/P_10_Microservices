namespace Identity.Domain
{
    public class AuthToken
    {
        public string Token { get; set; }
        public int ExpirationTime { get; set; }
    }
}
