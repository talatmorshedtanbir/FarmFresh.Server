using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Orders;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order, long, FrameworkContext>
    {
    }
}
