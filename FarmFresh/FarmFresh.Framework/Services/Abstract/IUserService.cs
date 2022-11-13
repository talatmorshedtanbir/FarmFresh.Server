using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Models.Requests;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IUserService : IDisposable
    {
        Task<User> GetAsync(string email);
        Task AddAsync(AddUserRequest userRequest);
    }
}
