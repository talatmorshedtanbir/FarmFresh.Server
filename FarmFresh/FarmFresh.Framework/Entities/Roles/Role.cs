using FarmFresh.Data;

namespace FarmFresh.Framework.Entities.Roles
{
    public class Role : IEntity<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
