using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    internal class OrderItemRepository : Repository<OrderItem, int, FrameworkContext>, IOrderItemRepository
    {
        public OrderItemRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
