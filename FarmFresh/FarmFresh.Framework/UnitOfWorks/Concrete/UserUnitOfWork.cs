using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class UserUnitOfWork : UnitOfWork, IUserUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }
        public UserUnitOfWork(FrameworkContext dbContext, IUserRepository userRepository) : base(dbContext)
        {
            UserRepository = userRepository;
        }
    }
}
