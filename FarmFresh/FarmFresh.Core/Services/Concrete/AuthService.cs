using FarmFresh.Core.Models.Requests;
using FarmFresh.Core.Services.Abstract;
using FarmFresh.Framework.Services.Abstract;
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
            var user = await userService.GetUserAsync(loginRequest.Email);

            if (user is null || user.Password != loginRequest.Password)
            {
                return false;
            }

            return true;
        }
    }
}
