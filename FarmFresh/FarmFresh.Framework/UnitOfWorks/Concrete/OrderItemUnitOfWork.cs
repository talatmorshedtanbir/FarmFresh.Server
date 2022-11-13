using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class OrderItemUnitOfWork : UnitOfWork, IOrderItemUnitOfWork
    {
        public IOrderItemRepository OrderItemRepository { get; set; }
        public OrderItemUnitOfWork(FrameworkContext dbContext, IOrderItemRepository orderItemRepository) : base(dbContext)
        {
            OrderItemRepository = orderItemRepository;
        }
    }
}
