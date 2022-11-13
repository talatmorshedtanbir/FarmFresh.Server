using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Stores;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IStoreRepository : IRepository<Store, long, FrameworkContext>
    {
    }
}
