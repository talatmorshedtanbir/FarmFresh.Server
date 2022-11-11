using FarmFresh.Data;
using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Entities.Carts
{
    public class CustomerCart : IEntity<int>
    {
        public Guid CustomerId { get; set; }

        public virtual User Customer { get; set; }

        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
