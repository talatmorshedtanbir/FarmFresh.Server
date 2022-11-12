using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Concrete
{
    public class CategoryUnitOfWork : UnitOfWork, ICategoryUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; set; }
        public CategoryUnitOfWork(FrameworkContext dbContext, ICategoryRepository categoryRepository) : base(dbContext)
        {
            CategoryRepository = categoryRepository;
        }
    }
}
