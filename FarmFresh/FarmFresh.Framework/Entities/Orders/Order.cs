using FarmFresh.Data;

namespace FarmFresh.Framework.Entities.Orders
{
    public class Order : IAuditableEntity<long>
    {
        public decimal TotalCost { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
