using FarmFresh.Data;
using FarmFresh.Framework.Entities.Products;

namespace FarmFresh.Framework.Entities.Stores
{
    public class ProductStore : IEntity<long>
    {
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}
