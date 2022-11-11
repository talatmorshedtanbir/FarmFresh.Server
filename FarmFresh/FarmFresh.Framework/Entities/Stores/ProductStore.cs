using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Stores
{
    public class ProductStore : IEntity<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}
