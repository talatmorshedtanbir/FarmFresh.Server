using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface ICartItemUnitOfWork : IUnitOfWork
    {
        public ICartItemRepository CartItemRepository { get; set; }
    }
}
