using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class CartUnitOfWork : UnitOfWork, ICartUnitOfWork
    {
        public ICartRepository CartRepository { get; set; }
        public CartUnitOfWork(FrameworkContext dbContext, ICartRepository cartRepository) : base(dbContext)
        {
            CartRepository = cartRepository;
        }
    }
}
