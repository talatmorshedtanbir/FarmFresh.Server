using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Categories;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, int, FrameworkContext>
    {
    }
}
