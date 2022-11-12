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

        public async Task<(IEnumerable<Category> Items, int Total, int TotalFilter)> GetAllAsync(
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

        public async Task AddAsync(AddCategoryRequest categoryRequest)
        {
            if (categoryRequest is null)
            {
                throw new NullRequestException(nameof(categoryRequest));
            }

            var isExists = await _categoryUnitOfWork.CategoryRepository.IsExistsAsync(
                x => x.CategoryName == categoryRequest.CategoryName);

            if (isExists)
                throw new DuplicationException(nameof(categoryRequest.CategoryName));

            var categoryToAdd = new Category
            {
                CategoryName = categoryRequest.CategoryName
            };

            await _categoryUnitOfWork.CategoryRepository.AddAsync(categoryToAdd);
            await _categoryUnitOfWork.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdateCategoryRequest categoryRequest)
        {
            if (categoryRequest is null)
            {
                throw new NullRequestException(nameof(categoryRequest));
            }

            var isExists = await _categoryUnitOfWork.CategoryRepository.IsExistsAsync(
                x => x.CategoryName == categoryRequest.CategoryName);

            if (isExists)
                throw new DuplicationException(nameof(categoryRequest.CategoryName));

            var categoryToUpdate = await GetByIdAsync(categoryRequest.Id);

            categoryToUpdate.CategoryName = categoryRequest.CategoryName;

            await _categoryUnitOfWork.CategoryRepository.UpdateAsync(categoryToUpdate);
            await _categoryUnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoryToDelete = await _categoryUnitOfWork.CategoryRepository.GetByIdAsync(id);

            if (categoryToDelete is null)
            {
                throw new NotFoundException(nameof(Category), nameof(id));
            }

            await _categoryUnitOfWork.CategoryRepository.DeleteAsync(categoryToDelete);
            await _categoryUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _categoryUnitOfWork?.Dispose();
        }
    }
}
