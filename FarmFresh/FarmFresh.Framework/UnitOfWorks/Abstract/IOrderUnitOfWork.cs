using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IOrderUnitOfWork : IUnitOfWork
    {
        public IOrderRepository OrderRepository { get; set; }
    }
}
