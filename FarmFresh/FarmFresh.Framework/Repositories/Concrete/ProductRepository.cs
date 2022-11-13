using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Framework.Repositories.Concrete
{
    public class ProductRepository : Repository<Product, long, FrameworkContext>, IProductRepository
    {
        public ProductRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<Product> GetAsync(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}
