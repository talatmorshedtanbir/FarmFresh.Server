using FarmFresh.Core.Models.Filters;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Core.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductsFilter productFilter)
        {
            try
            {
                var products = await _productService.GetAllAsync(productFilter.SearchText,
                    productFilter.OrderBy,
                    productFilter.PageNumber,
                    productFilter.PageSize);

                var pagingInfo = new
                {
                    products.TotalFilter,
                    products.Total
                };

                return Ok(new
                {
                    products = (from item in products.Items
                                select new
                                {
                                    Categories = string.Join(", ", item.ProductCategories.Select(x => x.Category.CategoryName)),
                                    Title = item.Title,
                                    SubTitle = item.SubTitle,
                                    Description = item.Description,
                                    Country = item.Country,
                                    KeyInformation = item.KeyInformation,
                                    IsActive = item.IsActive,
                                    Price = item.Price,
                                    ImageBase64 = item.ImageBase64,
                                    Created = item.Created,
                                    LastModified = item.LastModified,
                                    Id = item.Id
                                }).ToList(),

                    pagingInfo = pagingInfo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new
                {
                    Result = "Error while retrieving data."
                }); ;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new
                {
                    Message = "Error while retrieving data."
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProductRequest productRequest)
        {
            try
            {
                await _productService.AddAsync(productRequest);

                _logger.LogInformation($"New product with title {productRequest.Title} is added.");

                return Ok(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new
                {
                    Result = "Fail"
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest updateProductRequest)
        {
            try
            {
                await _productService.UpdateAsync(updateProductRequest);

                _logger.LogInformation($"Updated product with id: {updateProductRequest.Id}.");

                return Ok(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new
                {
                    Result = "Fail"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);

                _logger.LogInformation($"Deleted product with id: {id}.");

                return Ok(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(new
                {
                    Result = "Fail"
                });
            }
        }
    }
}
