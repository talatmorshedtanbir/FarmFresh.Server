using FarmFresh.Core.Models.Requests;

namespace FarmFresh.Core.Services.Abstract
{
    public interface IUserService
    {
        Task<bool> AuthenticateUser(LoginRequest loginRequest);
    }
}
