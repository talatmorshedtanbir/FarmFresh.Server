using FarmFresh.Data;

namespace FarmFresh.Framework.Entities.Categories
{
    public class Category : IEntity<int>
    {
        public string CategoryName { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
