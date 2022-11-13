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

            this._categoryRepositoryMock.VerifyNoOtherCalls();
            this._categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }
    }
}
