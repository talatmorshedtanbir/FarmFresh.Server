using FarmFresh.Framework.Entities.Orders;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface IOrderService : IDisposable
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);

        Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, int orderId);

        Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerEmail);
    }
}
