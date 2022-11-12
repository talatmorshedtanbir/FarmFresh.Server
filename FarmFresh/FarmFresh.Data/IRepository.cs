using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FarmFresh.Data
{
    public interface IRepository<TEntity, TKey, TContext>
        where TEntity : IEntity<TKey>
        where TContext : DbContext
    {
        #region LINQ Async
        Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true);
        Task<(IList<TResult> Items, int Total, int TotalFilter)> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            int pageIndex = 1, int pageSize = 10,
                            bool disableTracking = true);
        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true);

        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IList<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IList<TEntity> entities);
        Task DeleteAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task DeleteRangeAsync(IList<TEntity> entities);
        #endregion

        #region LINQ
        IList<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true);
        (IList<TResult> Items, int Total, int TotalFilter) Get<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            int pageIndex = 1, int pageSize = 10,
                            bool disableTracking = true);
        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true);
        TEntity GetById(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> predicate = null);
        bool IsExists(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IList<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IList<TEntity> entities);
        void Delete(TKey id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        void DeleteRange(IList<TEntity> entities);
        #endregion

        #region SQL
        IList<TEntity> ExecuteSqlQuery(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        #endregion
    }
}
