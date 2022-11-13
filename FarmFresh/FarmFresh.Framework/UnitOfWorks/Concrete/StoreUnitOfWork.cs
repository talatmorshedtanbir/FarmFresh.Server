using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class StoreUnitOfWork : UnitOfWork, IStoreUnitOfWork
    {
        public IStoreRepository StoreRepository { get; set; }
        public StoreUnitOfWork(FrameworkContext dbContext, IStoreRepository storeRepository) : base(dbContext)
        {
            StoreRepository = storeRepository;
        }
    }
}
