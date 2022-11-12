using FarmFresh.Data;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product, int, FrameworkContext>
    {
        Task<Product> GetAsync(string title);
    }
}
