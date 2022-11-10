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

        public async Task<User> GetAsync(string email)
        {
            return await _userUnitOfWork.UserRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.Email == email,
                null,
                true);
        }

        public async Task AddAsync(User user)
        {
            var isExists = await _userUnitOfWork.UserRepository.IsExistsAsync(x => x.Email == user.Email ||
                x.Id != user.Id);
            //if (isExists)
            //    throw new DuplicationException(nameof(entity.Name));

            //await _groupUnitOfWork.GroupRepository.AddAsync(entity);
            //await _groupUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _userUnitOfWork?.Dispose();
        }
    }
}
