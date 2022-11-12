using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class ProductUnitOfWork : UnitOfWork, IProductUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
        public ProductUnitOfWork(FrameworkContext dbContext, IProductRepository productRepository) : base(dbContext)
        {
            ProductRepository = productRepository;
        }
    }
}
