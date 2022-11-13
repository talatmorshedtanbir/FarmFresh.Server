using FarmFresh.Framework.Entities.Categories;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace FarmFresh.Framework.Tests.Unit.Services.Concrete
{
    public partial class CategoryServiceTests
    {
        [Fact]
        public async Task DeleteAsync_NoErrorOccurs_ShouldDeleteCategory()
        {
            // given
            int randomCategoryId = GetRandomNumber();
            int inputCategoryId = randomCategoryId;
            Category randomCategory = GetRandomCategory();

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(repository =>
                repository.DeleteAsync(x => x.Id == inputCategoryId))
                    .Verifiable();

            // when
            await _categoryService
                .DeleteAsync(inputCategoryId);

            // then
            _categoryRepositoryMock.Verify(repository =>
                repository.DeleteAsync(It.IsAny<Expression<Func<Category, bool>>>()),
                    Times.Once);

            _categoryUnitOfWorkMock.Verify(repository =>
                repository.SaveChangesAsync(),
                    Times.Once);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }
    }
}
