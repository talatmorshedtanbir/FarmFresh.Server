using FarmFresh.Common.Exceptions;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;

namespace FarmFresh.Framework.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly IOrderItemUnitOfWork _orderItemUnitOfWork;
        private readonly ICustomerOrderUnitOfWork _customerOrderUnitOfWork;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public OrderService(IOrderUnitOfWork orderUnitOfWork,
            IOrderItemUnitOfWork orderItemUnitOfWork,
            ICustomerOrderUnitOfWork customerOrderUnitOfWork,
            IUserService userService,
            ICartService cartService)
        {
            _orderUnitOfWork = orderUnitOfWork;
            _orderItemUnitOfWork = orderItemUnitOfWork;
            _customerOrderUnitOfWork = customerOrderUnitOfWork;
            _userService = userService;
            _cartService = cartService;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            var order = await _orderUnitOfWork.OrderRepository.GetByIdAsync(orderId);
            var orderItems = order.OrderItems.AsEnumerable();

            return orderItems;
        }

        public async Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, int orderId)
        {
            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }

            var customerOrder = await _customerOrderUnitOfWork.CustomerOrderRepository.GetFirstOrDefaultAsync<CustomerOrder>(
                x => x,
                x => x.CustomerId == customer.Id && x.OrderId == orderId);

            return customerOrder;
        }

        public async Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerEmail)
        {
            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }

            var customerOrders = await _customerOrderUnitOfWork.CustomerOrderRepository.GetAsync(
                x => x.Order,
                x => x.CustomerId == customer.Id,
                x => x.OrderBy(o => o.Order.Created),
                null,
                true);

            return customerOrders;
        }

        public async Task<int> PlaceOrderAsync(string customerEmail,
            string customerPhone,
            string address)
        {
            var cartItems = await _cartService.GetCustomerCartItemsAsync(customerEmail);

            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }


            decimal cost = 0;
            List<string> productIds = new List<string>();

            foreach (var item in cartItems)
            {
                cost += item.Cost * item.Quantity;

                productIds.AddRange(
                    Enumerable.Repeat(
                        item.ProductId.ToString(),
                    item.Quantity).ToList());
            }

            var newOrder = new Order
            {

                Phone = customerPhone,
                Created = DateTime.Now,
                Address = address,
                TotalCost = cost
            };

            await _orderUnitOfWork.OrderRepository.AddAsync(newOrder);
            await _orderUnitOfWork.SaveChangesAsync();

            var newOrderItems = new List<OrderItem>(
                cartItems.Select(item =>
                new OrderItem
                {
                    ProductId = item.ProductId,
                    Cost = item.Cost,
                    Quantity = item.Quantity,
                    OrderId = newOrder.Id
                }).ToList());

            await _orderItemUnitOfWork.OrderItemRepository.AddRangeAsync(newOrderItems);
            await _orderItemUnitOfWork.SaveChangesAsync();

            var newCustomerOrders = new CustomerOrder
            {
                CustomerId = customer.Id,
                OrderId = newOrder.Id
            };

            await _customerOrderUnitOfWork.CustomerOrderRepository.AddAsync(newCustomerOrders);
            await _customerOrderUnitOfWork.SaveChangesAsync();

            await _cartService.EmptyCartAsync(customerEmail);

            return newOrder.Id;
        }

        public void Dispose()
        {
            _orderUnitOfWork?.Dispose();
            _orderItemUnitOfWork?.Dispose();
            _customerOrderUnitOfWork?.Dispose();
        }
    }
}
