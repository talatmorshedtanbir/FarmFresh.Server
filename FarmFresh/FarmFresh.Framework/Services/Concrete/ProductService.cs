using FarmFresh.Common.Exceptions;
using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.Services.Concrete
{
    public class ProductService : IProductService
    {
        private IProductUnitOfWork _productUnitOfWork;

        public ProductService(IProductUnitOfWork productUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productUnitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task AddAsync(AddProductRequest productRequest)
        {
            if (productRequest is null)
            {
                throw new NullRequestException(nameof(productRequest));
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

            if (productToUpdate is not null)
            {
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
            }

            await _productUnitOfWork.SaveChangesAsync();
        }


        public void Dispose()
        {
            _productUnitOfWork?.Dispose();
        }
    }
}
