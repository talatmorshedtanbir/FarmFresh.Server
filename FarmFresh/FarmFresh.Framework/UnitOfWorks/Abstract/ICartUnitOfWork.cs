using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface ICartUnitOfWork : IUnitOfWork
    {
        public ICartRepository CartRepository { get; set; }
    }
}
