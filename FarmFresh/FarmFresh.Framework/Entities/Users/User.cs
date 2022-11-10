using FarmFresh.Data;

namespace FarmFresh.Framework.Entities.Users
{
    public class User : IAuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
