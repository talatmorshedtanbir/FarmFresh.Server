using FarmFresh.Core.Models.Filters;
using FarmFresh.Framework.Models.Requests;
using FarmFresh.Framework.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Core.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger,
            ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();

                return Ok(categories);
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

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedAsync([FromQuery] CategoriesFilter categoryFilter)
        {
            try
            {
                var categories = await _categoryService.GetAllPaginatedAsync(categoryFilter.SearchText,
                    categoryFilter.OrderBy,
                    categoryFilter.PageNumber,
                    categoryFilter.PageSize);

                var pagingInfo = new
                {
                    categories.TotalFilter,
                    categories.Total
                };

                return Ok(new
                {
                    Categories = categories.Items,
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
                var category = await _categoryService.GetByIdAsync(id);

                return Ok(category);
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
        public async Task<IActionResult> Add([FromBody] AddCategoryRequest categoryRequest)
        {
            try
            {
                await _categoryService.AddAsync(categoryRequest);

                _logger.LogInformation($"New category with name {categoryRequest.CategoryName} is added.");

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
        public async Task<IActionResult> Update(UpdateCategoryRequest updateCategoryRequest)
        {
            try
            {
                await _categoryService.UpdateAsync(updateCategoryRequest);

                _logger.LogInformation($"Updated category with id: {updateCategoryRequest.Id}.");

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
                await _categoryService.DeleteAsync(id);

                _logger.LogInformation($"Deleted category with id: {id}.");

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
