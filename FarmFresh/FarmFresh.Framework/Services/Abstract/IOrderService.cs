using FarmFresh.Framework.Entities.Orders;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IOrderService : IDisposable
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(long orderId);

        Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, long orderId);

        Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerEmail);

        Task<long> PlaceOrderAsync(string customerEmail,
            string customerPhone,
            string address);
    }
}
