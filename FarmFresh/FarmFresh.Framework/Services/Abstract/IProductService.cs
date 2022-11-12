using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Models.Responses;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IProductService : IDisposable
    {
        Task<(IEnumerable<ProductResponse> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize);

        Task<ProductResponse> GetByIdAsync(int id);

        Task AddAsync(AddProductRequest productRequest);

        Task UpdateAsync(UpdateProductRequest updateProductRequest);

        Task DeleteAsync(int id);
    }
}
