using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class CustomerCartRepository : Repository<CustomerCart, long, FrameworkContext>, ICustomerCartRepository
    {
        public CustomerCartRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
