using FarmFresh.Common.Exceptions;
using FarmFresh.Framework.Entities.Roles;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using Microsoft.AspNetCore.Identity;

namespace FarmFresh.Framework.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly IUserRoleUnitOfWork _userRoleUnitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserUnitOfWork userUnitOfWork,
            IUserRoleUnitOfWork userRoleUnitOfWork)
        {
            _userUnitOfWork = userUnitOfWork;
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> GetAsync(string email)
        {
            return await _userUnitOfWork.UserRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.Email == email);
        }

        public async Task AddAsync(AddUserRequest userRequest)
        {
            if (userRequest is null)
            {
                throw new NullRequestException(nameof(AddUserRequest));
            }

            var doesExist = await _userUnitOfWork.UserRepository.IsExistsAsync(x => x.Email == userRequest.Email);

            if (doesExist)
            {
                throw new DuplicationException(nameof(userRequest.Email));
            }

            var newUser = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                Phone = userRequest.Phone,
                Created = DateTime.Now,
                CreatedBy = userRequest.CreatedBy,
                IsActive = true,
                IsDeleted = false
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, userRequest.Password);

            await _userUnitOfWork.UserRepository.AddAsync(newUser);
            await _userUnitOfWork.SaveChangesAsync();

            if (userRequest.Roles is not null)
            {
                foreach (var role in userRequest.Roles)
                {
                    var userRole = new UserRole
                    {
                        UserId = newUser.Id,
                        RoleId = role
                    };

                    await _userRoleUnitOfWork.UserRoleRepository.AddAsync(userRole);
                }

                await _userRoleUnitOfWork.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _userUnitOfWork?.Dispose();
        }
    }
}
