using FarmFresh.Data;

namespace FarmFresh.Framework.Entities.Stores
{
    public class Store : IAuditableEntity<int>
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public virtual ICollection<ProductStore> ProductStores { get; set; }
    }
}
