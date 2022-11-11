using FarmFresh.Data;
using FarmFresh.Framework.Repositories.Abstract;

namespace FarmFresh.Framework.UnitOfWorks.Abstract
{
    public interface IProductUnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
    }
}
