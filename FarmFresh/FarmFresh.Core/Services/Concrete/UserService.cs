using FarmFresh.Core.Models.Requests;
using FarmFresh.Core.Services.Abstract;
using IConfigurationProvider = FarmFresh.Core.Providers.Abstract.IConfigurationProvider;

namespace FarmFresh.Core.Services.Concrete
{
    public class UserService : IUserService
    {
        private IConfigurationProvider configurationProvider;

        public UserService(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        public async Task<bool> AuthenticateUser(LoginRequest loginRequest)
        {
            var authenticationResponse = new LoginRequest
            {
                Email = "talat@gmail.com",
                Password = "123456"
            };

            if (authenticationResponse is null)
            {
                throw new Exception("Wrong username or password, authentication failed");
            }

            return true;
        }
    }
}
