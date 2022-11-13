using FarmFresh.Common.Constants;
using FarmFresh.Common.Exceptions;
using FarmFresh.Core.Services.Abstract;
using FarmFresh.Framework.DataSeeds;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;

namespace FarmFresh.Core.Services.Concrete
{
    public class DataSeederService : IDataSeederService
    {
        private readonly IUserService _userService;

        public DataSeederService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SeedUserData()
        {
            var userSeeds = UserSeeds.GetUserSeeds();

            foreach (var userSeed in userSeeds)
            {
                var userToAdd = new AddUserRequest
                {
                    Email = userSeed.Email,
                    Name = userSeed.Name,
                    CreatedBy = userSeed.CreatedBy,
                    Password = userSeed.Password,
                    Phone = userSeed.Phone,
                    Roles = new List<long>
                    {
                        (long)UserRoleConstants.Admin,
                        (long)UserRoleConstants.User
                    }
                };

                try
                {
                    await _userService.AddAsync(userToAdd);
                }
                catch (DuplicationException)
                {
                }
            }
        }
    }
}
