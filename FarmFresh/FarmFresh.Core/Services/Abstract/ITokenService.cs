using System.Security.Claims;

namespace FarmFresh.Core.Services.Abstract
{
    public interface ITokenService
    {
        string BuildToken();
        (string, double) BuildToken(Claim[] claims);
        bool IsValidToken(string token);
    }
}
