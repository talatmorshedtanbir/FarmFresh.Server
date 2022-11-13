using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.Services.Concrete;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FarmFresh.Framework.Tests.Unit.Services.Concrete
{
    [ExcludeFromCodeCoverage]
    public class CategoryServiceTests
    {
        private readonly ICategoryService _categoryService;
        private readonly Mock<ICategoryUnitOfWork> _categoryUnitOfWorkMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private static Random random;

        public CategoryServiceTests()
        {
            _categoryUnitOfWorkMock = new Mock<ICategoryUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryUnitOfWorkMock.Object);
            random = new Random();
        }

        public IEnumerable<Category> GetRandomCategories()
        {
            var randomCategories = Enumerable.Range(0, GetRandomNumber())
                .Select(i => new Category()
                {
                    Id = i + 1,
                    CategoryName = GetRandomString(),
                    IsActive = true,
                    IsDeleted = false
                }).ToList();

            return randomCategories;
        }

        public Category GetRandomCategory()
        {
            var randomCategory = new Category()
            {
                Id = GetRandomNumber(1, 100),
                CategoryName = GetRandomString(),
                IsActive = true,
                IsDeleted = false
            };

            return randomCategory;
        }

        public AddCategoryRequest CreateCategoryAddRequest(Category category)
        {
            var categoryRequest = new AddCategoryRequest
            {
                CategoryName = category.CategoryName,
            };

            return categoryRequest;
        }

        public UpdateCategoryRequest CreateCategoryUpdateRequestDto(Category category)
        {
            var categoryRequest = new UpdateCategoryRequest
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };

            return categoryRequest;
        }

        private string GetRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var length = GetRandomNumber();

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private int GetRandomNumber() => random.Next(2, 20);

        public int GetRandomNumber(int low, int high) => random.Next(low, high);
    }
}
