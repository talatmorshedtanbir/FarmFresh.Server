using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.DataSeeds
{
    public static class UserSeeds
    {
        public static IEnumerable<User> GetUserSeeds()
        {
            return Users;
        }

        private static IEnumerable<User> Users
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
                        Phone = "+8801817316436",
                        Password = "P@ssw0rd"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "user@haulio.com",
                        Name = "Tanbir",
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Phone = "+8801817316436",
                        Password = "P@ssw0rd"
                    }
                };
            }
        }
    }
}
