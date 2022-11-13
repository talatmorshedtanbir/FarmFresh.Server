using FarmFresh.Data;
using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Entities.Orders
{
    public class CustomerOrder : IEntity<long>
    {
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        public Guid CustomerId { get; set; }

        public virtual User Customer { get; set; }
    }
}
