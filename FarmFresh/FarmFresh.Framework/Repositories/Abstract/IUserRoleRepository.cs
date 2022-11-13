using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Roles;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IUserRoleRepository : IRepository<UserRole, long, FrameworkContext>
    {
    }
}
