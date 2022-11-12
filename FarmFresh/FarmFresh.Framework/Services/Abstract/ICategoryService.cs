using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Models.Requests;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface ICategoryService : IDisposable
    {
        Task<(IEnumerable<Category> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize);

        Task<Category> GetByIdAsync(int id);

        Task AddAsync(AddCategoryRequest categoryRequest);

        Task UpdateAsync(UpdateCategoryRequest categoryRequest);

        Task DeleteAsync(int id);
    }
}
