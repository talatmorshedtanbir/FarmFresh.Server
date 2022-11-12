using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IProductCategoryUnitOfWork : IUnitOfWork
    {
        public IProductCategoryRepository ProductCategoryRepository { get; set; }
    }
}
