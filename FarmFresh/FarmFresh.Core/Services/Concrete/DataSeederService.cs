using FarmFresh.Core.Services.Abstract;
using FarmFresh.Framework.DataSeeds;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace FarmFresh.Core.Services.Concrete
{
    public class DataSeederService : IDataSeederService
    {
        private readonly IUserService userService;

        public DataSeederService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task SeedUserData()
        {
            var seedData = new UserSeeds(new PasswordHasher<User>());

            //await userService
            //context.Database.EnsureCreated();

            //var user = context.User.FirstOrDefault();
            //if (user == null)
            //{
            //    context.User.AddRange(seedData.GetUsers());
            //}
            //context.SaveChanges();
        }
    }
}
