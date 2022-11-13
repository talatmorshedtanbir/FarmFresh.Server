using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IStoreUnitOfWork : IUnitOfWork
    {
        public IStoreRepository StoreRepository { get; set; }
    }
}
