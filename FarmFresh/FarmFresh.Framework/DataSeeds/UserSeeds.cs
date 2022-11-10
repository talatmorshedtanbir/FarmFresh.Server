﻿using FarmFresh.Framework.Entities.Users;
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

        public List<User> GetUserSeeds()
        {
            var users = new List<User>();
            foreach (var user in Users)
            {
                user.Password = _passwordHasher.HashPassword(user, "P@ssw0rd");
                users.Add(user);
            }

            return users;
        }

        private List<User> Users
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
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "user@haulio.com",
                        Name = "Tanbir",
                        Created = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                    }
                };
            }
        }
    }
}
