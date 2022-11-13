using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IUserRoleUnitOfWork : IUnitOfWork
    {
        public IUserRoleRepository UserRoleRepository { get; set; }
    }
}
