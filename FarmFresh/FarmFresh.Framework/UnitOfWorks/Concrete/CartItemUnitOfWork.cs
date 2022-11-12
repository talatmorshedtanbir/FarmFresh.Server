using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class CartItemUnitOfWork : UnitOfWork, ICartItemUnitOfWork
    {
        public ICartItemRepository CartItemRepository { get; set; }
        public CartItemUnitOfWork(FrameworkContext dbContext, ICartItemRepository cartItemRepository) : base(dbContext)
        {
            CartItemRepository = cartItemRepository;
        }
    }
}
