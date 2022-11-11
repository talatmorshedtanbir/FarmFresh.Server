using FarmFresh.Data;
using FarmFresh.Framework.Entities.Roles;

namespace FarmFresh.Framework.Entities.Users
{
    public class User : IAuditableEntity<Guid>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
