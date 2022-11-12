using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface ICategoryUnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; set; }
    }
}
