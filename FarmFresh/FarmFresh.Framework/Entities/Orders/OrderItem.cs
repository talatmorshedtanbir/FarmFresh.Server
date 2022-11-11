using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Orders
{
    public class OrderItem : IEntity<int>
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        // Navigations Lazy Loading
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
