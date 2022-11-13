using FarmFresh.Framework.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace FarmFresh.Framework.DataSeeds
{
    public class UserSeeds
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserSeeds(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public IEnumerable<User> GetUserSeeds()
        {
            return Users;
        }

        private IEnumerable<User> Users
        {
            get
            {
                return new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin@haulio.com",
                        Name = "Talat",
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Phone = "+8801817316436"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "user@haulio.com",
                        Name = "Tanbir",
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Phone = "+8801817316436"
                    }
                };
            }
        }
    }
}
