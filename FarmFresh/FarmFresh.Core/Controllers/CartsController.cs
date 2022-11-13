using FarmFresh.Framework.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Core.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> _logger;
        private readonly ICartService _cartService;

        public CartsController(ILogger<CartsController> logger,
            ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpGet("getcartitemsbycart/{cartId}")]
        public async Task<IActionResult> GetCartItemsAsync(int cartId)
        {
            try
            {
                var carts = await _cartService.GetCartItemsAsync(cartId);

                return Ok(carts);
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

        [HttpGet("getcartitemsbyuser/{customerEmail}")]
        public async Task<IActionResult> GetCustomerCartItemsAsync(string customerEmail)
        {
            try
            {
                var carts = await _cartService.GetCustomerCartItemsAsync(customerEmail);

                return Ok(carts);
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

        [HttpPost("addcartitem/{customerEmail}/{productId}")]
        public async Task<IActionResult> AddCartItemAsync(string customerEmail, int productId)
        {
            try
            {
                await _cartService.AddCartItemAsync(customerEmail, productId);

                _logger.LogInformation($"New cart is added.");

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

        [HttpPost("{customerEmail}")]
        public async Task<IActionResult> AddCartAsync(string customerEmail)
        {
            try
            {
                await _cartService.AddCartAsync(customerEmail);

                _logger.LogInformation($"New cart is added.");

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

        [HttpDelete("deletecartitem/{customerEmail}/{productId}")]
        public async Task<IActionResult> DeleteCartItemAsync(string customerEmail, int productId)
        {
            try
            {
                await _cartService.DeleteCartItemAsync(customerEmail, productId);

                _logger.LogInformation($"Deleted cart with product id: {productId}.");

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
