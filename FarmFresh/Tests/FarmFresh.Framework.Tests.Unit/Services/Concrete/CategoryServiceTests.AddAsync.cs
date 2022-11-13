using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Models.Requests;
using Force.DeepCloner;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace FarmFresh.Framework.Tests.Unit.Services.Concrete
{
    public partial class CategoryServiceTests
    {
        [Fact]
        public async Task AddAsync_NoErrorOccurs_ShouldAddCategory()
        {
            // given
            int randomCategoryId = GetRandomNumber();
            int inputCategoryId = randomCategoryId;
            Category randomCategory = GetRandomCategory();
            Category repositoryCategory = randomCategory.DeepClone();
            AddCategoryRequest inputCategory = CreateCategoryAddRequest(randomCategory);

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(repository =>
                repository.IsExistsAsync(x => x.CategoryName == inputCategory.CategoryName))
                    .ReturnsAsync(false);

            _categoryRepositoryMock.Setup(repository =>
                repository.AddAsync(repositoryCategory))
                    .Verifiable();

            // when
            await _categoryService
                .AddAsync(inputCategory);

            // then
            _categoryRepositoryMock.Verify(repository =>
                repository.IsExistsAsync(It.IsAny<Expression<Func<Category, bool>>>()),
                    Times.Once);

            _categoryRepositoryMock.Verify(repository =>
                repository.AddAsync(It.IsAny<Category>()),
                    Times.Once);

            _categoryUnitOfWorkMock.Verify(repository =>
                repository.SaveChangesAsync(),
                    Times.Once);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }
    }
}
