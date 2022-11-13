using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Orders
{
    public class OrderItem : IEntity<long>
    {
        public long ProductId { get; set; }

        public long OrderId { get; set; }

        public long Quantity { get; set; }

        public decimal Cost { get; set; }

        // Navigations Lazy Loading
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
