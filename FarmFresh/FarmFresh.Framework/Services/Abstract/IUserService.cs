using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IUserService : IDisposable
    {
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task AddRangeAsync(IList<User> users);
    }
}
