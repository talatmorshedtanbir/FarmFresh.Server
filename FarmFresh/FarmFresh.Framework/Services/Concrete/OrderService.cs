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

        public void Dispose()
        {
            _orderUnitOfWork?.Dispose();
            _orderItemUnitOfWork?.Dispose();
            _customerOrderUnitOfWork?.Dispose();
        }
    }
}
