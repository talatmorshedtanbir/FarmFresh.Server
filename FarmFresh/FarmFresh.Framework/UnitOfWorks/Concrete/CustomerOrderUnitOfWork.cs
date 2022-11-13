using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class CustomerOrderUnitOfWork : UnitOfWork, ICustomerOrderUnitOfWork
    {
        public ICustomerOrderRepository CustomerOrderRepository { get; set; }
        public CustomerOrderUnitOfWork(FrameworkContext dbContext, ICustomerOrderRepository customerOrderRepository) : base(dbContext)
        {
            CustomerOrderRepository = customerOrderRepository;
        }
    }
}
