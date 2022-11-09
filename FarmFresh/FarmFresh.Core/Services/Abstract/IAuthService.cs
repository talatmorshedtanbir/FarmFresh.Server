using FarmFresh.Core.Models.Requests;

namespace FarmFresh.Core.Services.Abstract
{
    public interface IAuthService
    {
        Task<bool> AuthenticateUser(LoginRequest loginRequest);
    }
}
