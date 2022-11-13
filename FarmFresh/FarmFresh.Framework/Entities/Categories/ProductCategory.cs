using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Categories
{
    public class ProductCategory : IEntity<long>
    {
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
