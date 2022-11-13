using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Orders;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface ICustomerOrderRepository : IRepository<CustomerOrder, long, FrameworkContext>
    {
    }
}
