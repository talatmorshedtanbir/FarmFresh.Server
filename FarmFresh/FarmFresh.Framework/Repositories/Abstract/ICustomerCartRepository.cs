using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Carts;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface ICustomerCartRepository : IRepository<CustomerCart, long, FrameworkContext>
    {
    }
}
