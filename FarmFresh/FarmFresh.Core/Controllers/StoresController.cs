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
    public class StoresController : Controller
    {
        private readonly ILogger<StoresController> _logger;
        private readonly IStoreService _storeService;

        public StoresController(ILogger<StoresController> logger,
            IStoreService storeService)
        {
            _logger = logger;
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] StoresFilter storeFilter)
        {
            try
            {
                var stores = await _storeService.GetAllAsync(storeFilter.SearchText,
                    storeFilter.OrderBy,
                    storeFilter.PageNumber,
                    storeFilter.PageSize);

                var pagingInfo = new
                {
                    stores.TotalFilter,
                    stores.Total
                };

                return Ok(new
                {
                    Stores = stores.Items,
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
                var store = await _storeService.GetByIdAsync(id);

                return Ok(store);
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
        public async Task<IActionResult> Add([FromBody] AddStoreRequest storeRequest)
        {
            try
            {
                await _storeService.AddAsync(storeRequest);

                _logger.LogInformation($"New store with name {storeRequest.Name} is added.");

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
        public async Task<IActionResult> Update(UpdateStoreRequest updateStoreRequest)
        {
            try
            {
                await _storeService.UpdateAsync(updateStoreRequest);

                _logger.LogInformation($"Updated store with id: {updateStoreRequest.Id}.");

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
                await _storeService.DeleteAsync(id);

                _logger.LogInformation($"Deleted store with id: {id}.");

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
