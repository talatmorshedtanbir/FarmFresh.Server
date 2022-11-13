using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Roles;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class UserRoleRepository : Repository<UserRole, long, FrameworkContext>, IUserRoleRepository
    {
        public UserRoleRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
