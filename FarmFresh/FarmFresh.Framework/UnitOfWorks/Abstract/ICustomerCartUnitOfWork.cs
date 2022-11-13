using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface ICustomerCartUnitOfWork : IUnitOfWork
    {
        public ICustomerCartRepository CustomerCartRepository { get; set; }
    }
}
