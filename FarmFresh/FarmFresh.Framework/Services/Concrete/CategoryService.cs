using FarmFresh.Common.Exceptions;
using FarmFresh.Common.Extensions;
using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using System.Linq.Expressions;

namespace FarmFresh.Framework.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryUnitOfWork _categoryUnitOfWork;

        public CategoryService(ICategoryUnitOfWork categoryUnitOfWork)
        {
            _categoryUnitOfWork = categoryUnitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryUnitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<Category> Items, int Total, int TotalFilter)> GetAllPaginatedAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Category, object>>>()
            {
                ["CategoryName"] = v => v.CategoryName
            };

            var result = await _categoryUnitOfWork.CategoryRepository.GetAsync<Category>(
                x => x, x => x.CategoryName.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), null,
                pageIndex, pageSize, disableTracking: true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            var category = await _categoryUnitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException(nameof(Category), nameof(id));
            }

            return category;
        }

        public async Task AddAsync(AddCategoryRequest categoryRequest)
        {
            if (categoryRequest is null)
            {
                throw new NullRequestException(nameof(AddCategoryRequest));
            }

            var isExists = await _categoryUnitOfWork.CategoryRepository.IsExistsAsync(
                x => x.CategoryName == categoryRequest.CategoryName);

            if (isExists)
            {
                throw new DuplicationException(nameof(Category));
            }

            var categoryToAdd = new Category
            {
                CategoryName = categoryRequest.CategoryName
            };

            await _categoryUnitOfWork.CategoryRepository.AddAsync(categoryToAdd);
            await _categoryUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCategoryRequest categoryRequest)
        {
            if (categoryRequest is null)
            {
                throw new NullRequestException(nameof(UpdateCategoryRequest));
            }

            var isExists = await _categoryUnitOfWork.CategoryRepository.IsExistsAsync(
                x => x.CategoryName == categoryRequest.CategoryName);

            if (isExists)
            {
                throw new DuplicationException(nameof(Category));
            }

            var categoryToUpdate = await GetByIdAsync(categoryRequest.Id);

            if (categoryToUpdate is null)
            {
                throw new NotFoundException(nameof(categoryToUpdate), nameof(categoryToUpdate.Id));
            }

            categoryToUpdate.CategoryName = categoryRequest.CategoryName;

            await _categoryUnitOfWork.CategoryRepository.UpdateAsync(categoryToUpdate);
            await _categoryUnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _categoryUnitOfWork.CategoryRepository.DeleteAsync(x => x.Id == id);
            await _categoryUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _categoryUnitOfWork?.Dispose();
        }
    }
}
