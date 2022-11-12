using FarmFresh.Framework.Entities.Products;
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

        public async Task UpdateAsync(Product product)
        {
            var productToUpdate = await _productUnitOfWork.ProductRepository.GetByIdAsync(product.Id);

            if (productToUpdate is not null)
            {
                productToUpdate.Price = product.Price;
                productToUpdate.LastModified = DateTime.Now;
                productToUpdate.SubTitle = product.SubTitle;
                productToUpdate.Title = product.Title;
                productToUpdate.Country = product.Country;
                productToUpdate.Description = product.Description;
                productToUpdate.KeyInformation = product.KeyInformation;
                productToUpdate.ImageBase64 = productToUpdate.ImageBase64;
                productToUpdate.IsActive = productToUpdate.IsActive;

                await _productUnitOfWork.ProductRepository.UpdateAsync(productToUpdate);
            }
            else
            {
                var productToAdd = new Product
                {
                    Title = product.Title,
                    Country = product.Country,
                    SubTitle = product.SubTitle,
                    Description = product.Description,
                    Created = DateTime.Now,
                    ImageBase64 = product.ImageBase64,
                    KeyInformation = product.KeyInformation,
                    IsActive = true,
                    IsDeleted = false,
                    Price = product.Price,
                };

                await _productUnitOfWork.ProductRepository.AddAsync(productToAdd);
            }

            await _productUnitOfWork.SaveChangesAsync();
        }


        public void Dispose()
        {
            _productUnitOfWork?.Dispose();
        }

    }
}
