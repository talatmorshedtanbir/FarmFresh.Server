using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Carts;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface ICartItemRepository : IRepository<CartItem, int, FrameworkContext>
    {
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartItemId);
    }
}
