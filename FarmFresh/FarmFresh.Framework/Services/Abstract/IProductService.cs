using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task UpdateAsync(Product product);
    }
}
