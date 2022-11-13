using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class CartRepository : Repository<Cart, long, FrameworkContext>, ICartRepository
    {
        public CartRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
