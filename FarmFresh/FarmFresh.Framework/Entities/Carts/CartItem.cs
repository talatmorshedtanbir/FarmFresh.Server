using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Carts
{
    public class CartItem : IEntity<long>
    {
        public long ProductId { get; set; }

        public long CartId { get; set; }

        public long Quantity { get; set; }

        public decimal Cost { get; set; }

        // Navigations Lazy Loading
        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}
