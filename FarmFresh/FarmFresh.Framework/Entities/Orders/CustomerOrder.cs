using FarmFresh.Data;
using FarmFresh.Framework.Entities.Users;

namespace FarmFresh.Framework.Entities.Orders
{
    public class CustomerOrder : IEntity<int>
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public Guid CustomerId { get; set; }

        public virtual User Customer { get; set; }
    }
}
