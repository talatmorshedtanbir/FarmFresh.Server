using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class ProductRepository : Repository<Product, int, FrameworkContext>, IProductRepository
    {
        public ProductRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
