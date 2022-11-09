using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User, Guid, FrameworkContext>
    {
    }
}
