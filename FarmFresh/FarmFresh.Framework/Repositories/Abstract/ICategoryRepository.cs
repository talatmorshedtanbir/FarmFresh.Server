using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Categories;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<Category, long, FrameworkContext>
    {
    }
}
