using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IUserService : IDisposable
    {
        Task<User> GetUserAsync(string email);
    }
}
