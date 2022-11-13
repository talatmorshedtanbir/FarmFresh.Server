using FarmFresh.Common.Exceptions;
using FarmFresh.Framework.Entities.Categories;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace FarmFresh.Framework.Tests.Unit.Services.Concrete
{
    public partial class CategoryServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_NoErrorOccurs_ShouldReturnCategory()
        {
            // given
            int randomCategoryId = GetRandomNumber();
            int inputCategoryId = randomCategoryId;
            Category randomCategory = GetRandomCategory();
            Category repositoryCategory = randomCategory;
            Category expectedCategory = repositoryCategory.DeepClone();

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(repository =>
                repository.GetByIdAsync(inputCategoryId))
                    .ReturnsAsync(repositoryCategory);

            // when
            Category actualCategory =
                await _categoryService.GetByIdAsync(inputCategoryId);

            // then
            actualCategory.Should().BeEquivalentTo(expectedCategory);

            _categoryRepositoryMock.Verify(repository =>
                repository.GetByIdAsync(inputCategoryId),
                    Times.Once);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetByIdAsync_NoDataFound_ShouldThrowNotFoundException()
        {
            // given
            int randomCategoryId = GetRandomNumber();
            int inputCategoryId = randomCategoryId;
            Category repositoryCategory = null;

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(
                _categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(repository =>
                repository.GetByIdAsync(inputCategoryId))
                    .ReturnsAsync(repositoryCategory);

            // when
            Task<Category> getCategoryByIdTask =
                _categoryService.GetByIdAsync(inputCategoryId);

            // then
            await Assert.ThrowsAsync<NotFoundException>(() =>
                getCategoryByIdTask);

            _categoryRepositoryMock.Verify(repository =>
                repository.GetByIdAsync(It.IsAny<long>()),
                    Times.Once);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }
    }
}
