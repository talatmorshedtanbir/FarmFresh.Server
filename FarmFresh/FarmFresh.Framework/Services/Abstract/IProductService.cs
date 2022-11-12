using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Models.Requests;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IProductService : IDisposable
    {
        Task<(IEnumerable<Product> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize);

        Task<Product> GetByIdAsync(int id);

        Task AddAsync(AddProductRequest productRequest);

        Task UpdateAsync(UpdateProductRequest updateProductRequest);

        Task DeleteAsync(int id);
    }
}
