using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class UserRoleUnitOfWork : UnitOfWork, IUserRoleUnitOfWork
    {
        public IUserRoleRepository UserRoleRepository { get; set; }
        public UserRoleUnitOfWork(FrameworkContext dbContext, IUserRoleRepository userRoleRepository) : base(dbContext)
        {
            UserRoleRepository = userRoleRepository;
        }
    }
}
