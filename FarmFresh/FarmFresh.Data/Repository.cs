using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Linq.Expressions;

namespace FarmFresh.Data
{
    public abstract class Repository<TEntity, TKey, TContext>
        : IRepository<TEntity, TKey, TContext>
        where TEntity : IEntity<TKey>
        where TContext : DbContext
    {
        #region CONFIG
        protected TContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region LINQ Async
        public virtual async Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (disableTracking)
                query = query.AsNoTracking();

            var result = await query.Select(selector).ToListAsync();

            return result;
        }

        public virtual async Task<(IList<TResult> Items, int Total, int TotalFilter)> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            int pageIndex = 1, int pageSize = 10,
                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            int total = await query.CountAsync();
            int totalFilter = total;

            if (include != null)
                query = include(query);

            if (predicate != null)
            {
                query = query.Where(predicate);
                totalFilter = await query.CountAsync();
            }

            if (orderBy != null)
                query = orderBy(query);

            if (disableTracking)
                query = query.AsNoTracking();

            var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return (result, total, totalFilter);
        }

        public virtual async Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (disableTracking)
                query = query.AsNoTracking();

            var result = await query.Select(selector).FirstOrDefaultAsync();

            return result;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbSet.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }

        public virtual async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable();

            return await query.AnyAsync(predicate);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IList<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task UpdateRangeAsync(IList<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);

            await DeleteAsync(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable().Where(predicate);

            if (query.Any())
            {
                _dbSet.RemoveRange(query);
            }
        }

        public virtual async Task DeleteRangeAsync(IList<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }
        #endregion

        #region LINQ
        public virtual IList<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (disableTracking)
                query = query.AsNoTracking();

            var result = query.Select(selector).ToList();

            return result;
        }

        public virtual (IList<TResult> Items, int Total, int TotalFilter) Get<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            int pageIndex = 1, int pageSize = 10,
                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            int total = query.Count();
            int totalFilter = total;

            if (include != null)
                query = include(query);

            if (predicate != null)
            {
                query = query.Where(predicate);
                totalFilter = query.Count();
            }

            if (orderBy != null)
                query = orderBy(query);

            if (disableTracking)
                query = query.AsNoTracking();

            var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector).ToList();

            return (result, total, totalFilter);
        }

        public virtual TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                            Expression<Func<TEntity, bool>> predicate = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (disableTracking)
                query = query.AsNoTracking();

            var result = query.Select(selector).FirstOrDefault();

            return result;
        }

        public virtual TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbSet.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.Count();
        }

        public virtual bool IsExists(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable();

            return query.Any(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IList<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IList<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
        }

        public virtual void Delete(TKey id)
        {
            var entity = _dbSet.Find(id);

            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable().Where(predicate);

            if (query.Any())
            {
                _dbSet.RemoveRange(query);
            }
        }

        public virtual void DeleteRange(IList<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        #endregion

        #region SQL
        public IList<TEntity> ExecuteSqlQuery(string sql, params object[] parameters)
        {
            return (IList<TEntity>)_dbSet.FromSqlRaw(sql, parameters).ToListAsync();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        #endregion
    }
}
