using FarmFresh.Data;
using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Entities.Carts
{
    public class CustomerCart : IEntity<long>
    {
        public Guid CustomerId { get; set; }

        public virtual User Customer { get; set; }

        public long CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
