using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Categories
{
    public class ProductCategory : IEntity<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
