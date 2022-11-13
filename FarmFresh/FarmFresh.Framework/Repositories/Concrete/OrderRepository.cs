using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class OrderRepository : Repository<Order, int, FrameworkContext>, IOrderRepository
    {
        public OrderRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
