using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Carts
{
    public class CartItem : IEntity<int>
    {
        public int ProductId { get; set; }

        public int CartId { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        // Navigations Lazy Loading
        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}
