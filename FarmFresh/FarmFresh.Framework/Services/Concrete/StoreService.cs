using FarmFresh.Common.Exceptions;
using FarmFresh.Common.Extensions;
using FarmFresh.Framework.Entities.Stores;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using System.Linq.Expressions;

namespace FarmFresh.Framework.Services.Concrete
{
    public class StoreService : IStoreService
    {
        private readonly IStoreUnitOfWork _storeUnitOfWork;

        public StoreService(IStoreUnitOfWork storeUnitOfWork)
        {
            _storeUnitOfWork = storeUnitOfWork;
        }

        public async Task<(IEnumerable<Store> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Store, object>>>()
            {
                ["Name"] = v => v.Name
            };

            var result = await _storeUnitOfWork.StoreRepository.GetAsync<Store>(
                x => x, x => x.Name.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), null,
            pageIndex, pageSize, disableTracking: true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Store> GetByIdAsync(long id)
        {
            var store = await _storeUnitOfWork.StoreRepository.GetByIdAsync(id);

            if (store is null)
            {
                throw new NotFoundException(nameof(Store), nameof(id));
            }

            return store;
        }

        public async Task AddAsync(AddStoreRequest storeRequest)
        {
            if (storeRequest is null)
            {
                throw new NullRequestException(nameof(AddStoreRequest));
            }

            var isExists = await _storeUnitOfWork.StoreRepository.IsExistsAsync(
                x => x.Name == storeRequest.Name);

            if (isExists)
            {
                throw new DuplicationException(nameof(Store));
            }

            var storeToAdd = new Store
            {
                Name = storeRequest.Name,
                Location = storeRequest.Location,
                Created = DateTime.Now,
                CreatedBy = storeRequest.CreatedBy
            };

            await _storeUnitOfWork.StoreRepository.AddAsync(storeToAdd);
            await _storeUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateStoreRequest storeRequest)
        {
            if (storeRequest is null)
            {
                throw new NullRequestException(nameof(UpdateStoreRequest));
            }

            var isExists = await _storeUnitOfWork.StoreRepository.IsExistsAsync(
                x => x.Name == storeRequest.Name);

            if (isExists)
            {
                throw new DuplicationException(nameof(Store));
            }

            var storeToUpdate = await GetByIdAsync(storeRequest.Id);

            if (storeToUpdate is null)
            {
                throw new NotFoundException(nameof(storeToUpdate), nameof(storeToUpdate.Id));
            }

            storeToUpdate.Name = storeRequest.Name;
            storeToUpdate.Location = storeRequest.Location;
            storeToUpdate.LastModifiedBy = storeRequest.LastModifiedBy;
            storeToUpdate.LastModified = DateTime.Now;

            await _storeUnitOfWork.StoreRepository.UpdateAsync(storeToUpdate);
            await _storeUnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _storeUnitOfWork.StoreRepository.DeleteAsync(x => x.Id == id);
            await _storeUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _storeUnitOfWork?.Dispose();
        }
    }
}
