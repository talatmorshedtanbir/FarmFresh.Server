using FarmFresh.Data;
using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Entities.Stores;

namespace FarmFresh.Framework.Entities.Products
{
    public class Product : IAuditableEntity<int>
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string KeyInformation { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageBase64 { get; set; }

        public string Country { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<ProductStore> ProductStores { get; set; }
    }
}
