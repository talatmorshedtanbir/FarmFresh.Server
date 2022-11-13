using FarmFresh.Data;

namespace FarmFresh.Framework.Entities.Carts
{
    public class Cart : IAuditableEntity<long>
    {
        public decimal TotalCost { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<CustomerCart> CustomerCart { get; set; }
    }
}
