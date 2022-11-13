using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class OrderUnitOfWork : UnitOfWork, IOrderUnitOfWork
    {
        public IOrderRepository OrderRepository { get; set; }
        public OrderUnitOfWork(FrameworkContext dbContext, IOrderRepository orderRepository) : base(dbContext)
        {
            OrderRepository = orderRepository;
        }
    }
}
