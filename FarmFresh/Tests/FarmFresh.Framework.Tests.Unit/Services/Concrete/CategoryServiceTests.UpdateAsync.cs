using FarmFresh.Common.Exceptions;
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
        public async Task UpdateAsync_NoErrorOccurs_ShouldUpdateCategory()
        {
            // given
            Category randomCategory = GetRandomCategory();
            Category repositoryCategory = randomCategory.DeepClone();
            UpdateCategoryRequest inputCategory = CreateCategoryUpdateRequest(randomCategory);

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(repository =>
                repository.IsExistsAsync(x => x.CategoryName == inputCategory.CategoryName))
                    .ReturnsAsync(false);

            _categoryRepositoryMock.Setup(repository =>
                repository.GetByIdAsync(inputCategory.Id))
                    .ReturnsAsync(repositoryCategory);

            _categoryRepositoryMock.Setup(repository =>
                repository.UpdateAsync(repositoryCategory))
                    .Verifiable();

            // when
            await _categoryService
                .UpdateAsync(inputCategory);

            // then
            _categoryRepositoryMock.Verify(repository =>
                repository.IsExistsAsync(It.IsAny<Expression<Func<Category, bool>>>()),
                    Times.Once);

            _categoryRepositoryMock.Verify(repository =>
                repository.GetByIdAsync(It.IsAny<long>()),
                    Times.Once);

            _categoryRepositoryMock.Verify(repository =>
                repository.UpdateAsync(It.IsAny<Category>()),
                    Times.Once);

            _categoryUnitOfWorkMock.Verify(repository =>
                repository.SaveChangesAsync(),
                    Times.Once);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateAsync_RequestObjectIsNull_ShouldThrowNullRequestException()
        {
            // given
            UpdateCategoryRequest inputCategory = null;

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);

            // when
            Task updatetCategoryByIdTask =
                _categoryService.UpdateAsync(inputCategory);

            // then
            await Assert.ThrowsAsync<NullRequestException>(() =>
                updatetCategoryByIdTask);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateAsync_CategoryAlreadyExists_ShouldThrowDuplicationException()
        {
            // given
            Category randomCategory = GetRandomCategory();
            Category repositoryCategory = randomCategory.DeepClone();
            UpdateCategoryRequest inputCategory = CreateCategoryUpdateRequest(randomCategory);

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(repository =>
                repository.IsExistsAsync(x => x.CategoryName == inputCategory.CategoryName))
                    .ReturnsAsync(true);

            // when
            Task updatetCategoryByIdTask = _categoryService
                .UpdateAsync(inputCategory);

            // then
            _categoryRepositoryMock.Verify(repository =>
                repository.IsExistsAsync(It.IsAny<Expression<Func<Category, bool>>>()),
                    Times.Once);

            await Assert.ThrowsAsync<DuplicationException>(() =>
                updatetCategoryByIdTask);

            _categoryRepositoryMock.VerifyNoOtherCalls();
            _categoryUnitOfWorkMock.VerifyNoOtherCalls();
        }
    }
}
