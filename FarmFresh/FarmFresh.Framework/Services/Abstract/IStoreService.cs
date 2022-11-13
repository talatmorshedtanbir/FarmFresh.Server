using FarmFresh.Framework.Entities.Stores;
using FarmFresh.Framework.Models.Requests;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IStoreService : IDisposable
    {
        Task<(IEnumerable<Store> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize);

        Task<Store> GetByIdAsync(long id);

        Task AddAsync(AddStoreRequest storeRequest);

        Task UpdateAsync(UpdateStoreRequest storeRequest);

        Task DeleteAsync(long id);
    }
}
