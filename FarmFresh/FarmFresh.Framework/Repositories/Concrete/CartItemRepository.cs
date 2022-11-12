using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class CartItemRepository : Repository<CartItem, int, FrameworkContext>, ICartItemRepository
    {
        public CartItemRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
