using Microsoft.EntityFrameworkCore.Storage;

namespace FarmFresh.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
        public Task<IDbContextTransaction> BeginTransaction();
    }
}
