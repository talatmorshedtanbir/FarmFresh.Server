using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class CustomerCartUnitOfWork : UnitOfWork, ICustomerCartUnitOfWork
    {
        public ICustomerCartRepository CustomerCartRepository { get; set; }
        public CustomerCartUnitOfWork(FrameworkContext dbContext, ICustomerCartRepository customerCartRepository) : base(dbContext)
        {
            CustomerCartRepository = customerCartRepository;
        }
    }
}
