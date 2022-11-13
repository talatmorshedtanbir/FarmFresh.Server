using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IOrderItemUnitOfWork : IUnitOfWork
    {
        public IOrderItemRepository OrderItemRepository { get; set; }
    }
}
