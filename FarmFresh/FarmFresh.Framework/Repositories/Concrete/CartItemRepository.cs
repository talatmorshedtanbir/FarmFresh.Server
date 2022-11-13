using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class CartItemRepository : Repository<CartItem, long, FrameworkContext>, ICartItemRepository
    {
        public CartItemRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(long cartId)
        {
            return await _dbSet.Where(x => x.CartId == cartId).ToListAsync();
        }
    }
}
