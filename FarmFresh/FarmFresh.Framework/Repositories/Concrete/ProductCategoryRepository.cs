using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class ProductCategoryRepository : Repository<ProductCategory, int, FrameworkContext>, IProductCategoryRepository
    {
        public ProductCategoryRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
