using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class UserRepository : Repository<User, Guid, FrameworkContext>, IUserRepository
    {
        public UserRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
