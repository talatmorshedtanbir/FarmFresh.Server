using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface ICustomerOrderUnitOfWork : IUnitOfWork
    {
        public ICustomerOrderRepository CustomerOrderRepository { get; set; }
    }
}
