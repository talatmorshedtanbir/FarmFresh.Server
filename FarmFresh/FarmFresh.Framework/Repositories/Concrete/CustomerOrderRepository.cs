using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class CustomerOrderRepository : Repository<CustomerOrder, int, FrameworkContext>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
