using FarmFresh.Framework.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Core.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet("getorderitemsbyorder/{orderId}")]
        public async Task<IActionResult> GetOrderItemsAsync(int orderId)
        {
            try
            {
                var orderItems = await _orderService.GetOrderItemsAsync(orderId);

                return Ok(orderItems);
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

        [HttpGet("getbycustomerorder/{customerEmail}/{orderId}")]
        public async Task<IActionResult> GetCustomerOrderAsync(string customerEmail, int orderId)
        {
            try
            {
                var customerOrder = await _orderService.GetCustomerOrderAsync(customerEmail, orderId);

                return Ok(customerOrder);
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

        [HttpGet("getbycustomer/{customerEmail}")]
        public async Task<IActionResult> GetCustomerOrdersAsync(string customerEmail)
        {
            try
            {
                var customerOrders = await _orderService.GetCustomerOrdersAsync(customerEmail);

                return Ok(customerOrders);
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

        [HttpPost("{customerEmail}/{customerPhone}/{address}")]
        public async Task<IActionResult> PlaceOrderAsync(string customerEmail, string customerPhone, string address)
        {

            try
            {
                await _orderService.PlaceOrderAsync(customerEmail,
                    customerPhone,
                    address);

                _logger.LogInformation($"New order for customer {customerEmail} is added.");

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
