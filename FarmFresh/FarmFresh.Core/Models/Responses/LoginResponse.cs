namespace FarmFresh.Core.Models.Responses
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public double ExpiresAt { get; set; }
    }
}
