using FarmFresh.Core.Models.Requests;
using FarmFresh.Core.Services.Abstract;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using IConfigurationProvider = FarmFresh.Core.Providers.Abstract.IConfigurationProvider;

namespace FarmFresh.Core.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private IConfigurationProvider configurationProvider;
        private readonly IUserService userService;

        public AuthService(IConfigurationProvider configurationProvider,
            IUserService userService)
        {
            this.configurationProvider = configurationProvider;
            this.userService = userService;
        }

        public async Task<bool> AuthenticateUser(LoginRequest loginRequest)
        {
            var user = await userService.GetAsync(loginRequest.Email);

            var passwordHasher = new PasswordHasher<User>();

            if (user is null ||
                passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password) == PasswordVerificationResult.Failed)
            {
                return false;
            }

            return true;
        }
    }
}
