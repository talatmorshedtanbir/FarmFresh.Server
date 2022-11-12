using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Models.Requests;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task AddAsync(AddProductRequest productRequest);

        Task UpdateAsync(UpdateProductRequest updateProductRequest);
    }
}
