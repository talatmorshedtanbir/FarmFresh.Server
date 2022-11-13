using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    internal class OrderItemRepository : Repository<OrderItem, long, FrameworkContext>, IOrderItemRepository
    {
        public OrderItemRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
