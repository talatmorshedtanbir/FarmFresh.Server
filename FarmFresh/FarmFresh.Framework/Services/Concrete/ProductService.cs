using FarmFresh.Common.Exceptions;
using FarmFresh.Common.Extensions;
using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FarmFresh.Framework.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        private readonly IProductCategoryUnitOfWork _productCategoryUnitOfWork;

        public ProductService(IProductUnitOfWork productUnitOfWork,
            IProductCategoryUnitOfWork productCategoryUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork;
            _productCategoryUnitOfWork = productCategoryUnitOfWork;
        }


        public async Task<(IEnumerable<Product> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Product, object>>>()
            {
                ["Title"] = v => v.Title
            };

            var result = await _productUnitOfWork.ProductRepository.GetAsync<Product>(
                x => x, x => x.Title.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy),
                x => x.Include(i => i.ProductCategories).ThenInclude(i => i.Category),
                pageIndex, pageSize, disableTracking: true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productUnitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(AddProductRequest productRequest)
        {
            if (productRequest is null)
            {
                throw new NullRequestException(nameof(productRequest));
            }

            var isExists = await _productUnitOfWork.ProductRepository.IsExistsAsync(
                x => x.Title == productRequest.Title);

            if (isExists)
            {
                throw new DuplicationException(nameof(productRequest));
            }

            var productToAdd = new Product
            {
                Title = productRequest.Title,
                Country = productRequest.Country,
                SubTitle = productRequest.SubTitle,
                Description = productRequest.Description,
                Created = DateTime.Now,
                ImageBase64 = productRequest.ImageBase64,
                KeyInformation = productRequest.KeyInformation,
                IsActive = true,
                IsDeleted = false,
                Price = productRequest.Price,
            };

            await _productUnitOfWork.ProductRepository.AddAsync(productToAdd);
            await _productUnitOfWork.SaveChangesAsync();

            var createdProduct = await _productUnitOfWork.ProductRepository.GetAsync(productToAdd.Title);

            if (createdProduct is not null && productRequest.Categories is not null &&
                productRequest.Categories.Count() > 0)
            {
                foreach (var category in productRequest.Categories)
                {
                    var productCategoryToAdd = new ProductCategory
                    {
                        CategoryId = category,
                        ProductId = createdProduct.Id,
                        IsActive = true,
                        IsDeleted = false
                    };

                    await _productCategoryUnitOfWork.ProductCategoryRepository.AddAsync(productCategoryToAdd);
                }

                await _productCategoryUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            if (updateProductRequest is null)
            {
                throw new NullRequestException(nameof(updateProductRequest));
            }

            var productToUpdate = await _productUnitOfWork.ProductRepository.GetByIdAsync(updateProductRequest.Id);

            if (productToUpdate is null)
            {
                throw new NotFoundException(nameof(productToUpdate), nameof(updateProductRequest.Id));
            }

            productToUpdate.Price = updateProductRequest.Price;
            productToUpdate.LastModified = DateTime.Now;
            productToUpdate.SubTitle = updateProductRequest.SubTitle;
            productToUpdate.Title = updateProductRequest.Title;
            productToUpdate.Country = updateProductRequest.Country;
            productToUpdate.Description = updateProductRequest.Description;
            productToUpdate.KeyInformation = updateProductRequest.KeyInformation;
            productToUpdate.ImageBase64 = productToUpdate.ImageBase64;
            productToUpdate.IsActive = productToUpdate.IsActive;

            await _productUnitOfWork.ProductRepository.UpdateAsync(productToUpdate);
            await _productUnitOfWork.SaveChangesAsync();

            await _productCategoryUnitOfWork.ProductCategoryRepository.DeleteAsync(
                x => x.Id == updateProductRequest.Id);

            if (updateProductRequest.Categories is not null &&
                updateProductRequest.Categories.Count() > 0)
            {
                foreach (var category in updateProductRequest.Categories)
                {
                    var productCategoryToAdd = new ProductCategory
                    {
                        CategoryId = category,
                        ProductId = updateProductRequest.Id,
                        IsActive = true,
                        IsDeleted = false
                    };

                    await _productCategoryUnitOfWork.ProductCategoryRepository.AddAsync(productCategoryToAdd);
                }
            }

            await _productCategoryUnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _productCategoryUnitOfWork.ProductCategoryRepository.DeleteAsync(x => x.ProductId == id);
            await _productCategoryUnitOfWork.SaveChangesAsync();

            await _productUnitOfWork.ProductRepository.DeleteAsync(x => x.Id == id);
            await _productUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _productUnitOfWork?.Dispose();
        }
    }
}
