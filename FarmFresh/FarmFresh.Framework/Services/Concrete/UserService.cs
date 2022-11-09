using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.Services.Concrete
{
    public class UserService : IUserService
    {
        private IUserUnitOfWork _userUnitOfWork;

        public UserService(IUserUnitOfWork userUnitOfWork)
        {
            _userUnitOfWork = userUnitOfWork;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _userUnitOfWork.UserRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.Email == email,
                null,
                true);
        }

        public void Dispose()
        {
            _userUnitOfWork?.Dispose();
        }
    }
}
