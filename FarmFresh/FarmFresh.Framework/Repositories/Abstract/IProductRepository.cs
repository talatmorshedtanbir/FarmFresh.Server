using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product, long, FrameworkContext>
    {
        Task<Product> GetAsync(string title);
    }
}
