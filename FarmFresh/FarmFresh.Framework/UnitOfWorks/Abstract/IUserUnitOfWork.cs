using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }
    }
}
