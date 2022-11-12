using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class ProductCategoryUnitOfWork : UnitOfWork, IProductCategoryUnitOfWork
    {
        public IProductCategoryRepository ProductCategoryRepository { get; set; }
        public ProductCategoryUnitOfWork(FrameworkContext dbContext,
            IProductCategoryRepository productCategoryRepository) : base(dbContext)
        {
            ProductCategoryRepository = productCategoryRepository;
        }
    }
}
