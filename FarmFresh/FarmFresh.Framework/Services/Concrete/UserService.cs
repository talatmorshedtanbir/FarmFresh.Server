using FarmFresh.Common.Exceptions;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserUnitOfWork _userUnitOfWork;

        public UserService(IUserUnitOfWork userUnitOfWork)
        {
            _userUnitOfWork = userUnitOfWork;
        }

        public async Task<User> GetAsync(string email)
        {
            return await _userUnitOfWork.UserRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.Email == email);
        }

        public async Task AddAsync(User user)
        {
            var doesExist = await _userUnitOfWork.UserRepository.IsExistsAsync(x => x.Email == user.Email ||
                x.Id != user.Id);
            if (doesExist)
                throw new DuplicationException(nameof(user.Email));

            await _userUnitOfWork.UserRepository.AddAsync(user);
            await _userUnitOfWork.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IList<User> users)
        {
            List<User> newUsers = new List<User>();

            foreach (var user in users)
            {
                var doesExist = await _userUnitOfWork.UserRepository.IsExistsAsync(x => x.Email == user.Email ||
                    x.Id != user.Id);

                if (doesExist is false)
                {
                    newUsers.Add(user);
                }
            }

            if (newUsers.Count > 0)
            {
                await _userUnitOfWork.UserRepository.AddRangeAsync(newUsers);
                await _userUnitOfWork.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _userUnitOfWork?.Dispose();
        }
    }
}
