using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Stores;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class StoreRepository : Repository<Store, long, FrameworkContext>, IStoreRepository
    {
        public StoreRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
