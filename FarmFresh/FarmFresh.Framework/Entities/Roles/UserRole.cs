using FarmFresh.Data;
using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Entities.Roles
{
    public class UserRole : IEntity<int>
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
