using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class CategoryRepository : Repository<Category, long, FrameworkContext>, ICategoryRepository
    {
        public CategoryRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
