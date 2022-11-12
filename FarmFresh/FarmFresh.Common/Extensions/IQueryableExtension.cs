using System.Linq.Expressions;

namespace FarmFresh.Common.Extensions
{
    public static class IQueryableExtension
    {
        public static IOrderedQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> query,
            Dictionary<string, Expression<Func<TEntity, object>>> columnsMap,
            string multipleSortBy)
        {
            IOrderedQueryable<TEntity> orderedQuery = (IOrderedQueryable<TEntity>)query;
            var orderByList = multipleSortBy.Split(',');

            if (!orderByList.Any()) return orderedQuery;

            var orderBy = orderByList[0];
            var prop = orderBy.Split(' ')[0];

            if (orderBy.Split(' ')[1] == "asc" && columnsMap.ContainsKey(prop))
                orderedQuery = orderedQuery.OrderBy(columnsMap[prop]);
            else if (columnsMap.ContainsKey(prop))
                orderedQuery = orderedQuery.OrderByDescending(columnsMap[prop]);
            else
                orderedQuery = orderedQuery.OrderBy(x => x);

            for (int i = 1; i < orderByList.Length; i++)
            {
                orderBy = orderByList[i];
                prop = orderBy.Split(' ')[0];

                if (orderBy.Split(' ')[1] == "asc" && columnsMap.ContainsKey(prop))
                    orderedQuery = orderedQuery.ThenBy(columnsMap[prop]);
                else if (columnsMap.ContainsKey(prop))
                    orderedQuery = orderedQuery.ThenByDescending(columnsMap[prop]);
            }

            return orderedQuery;
        }

        public static IOrderedQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> query,
            Dictionary<string, Expression<Func<TEntity, object>>> columnsMap,
            string sortBy,
            bool isAsc = true)
        {
            IOrderedQueryable<TEntity> orderedQuery = (IOrderedQueryable<TEntity>)query;

            if (string.IsNullOrEmpty(sortBy) || columnsMap == null || !columnsMap.ContainsKey(sortBy))
                return orderedQuery;

            if (isAsc)
                return orderedQuery.OrderBy(columnsMap[sortBy]);
            else
                return orderedQuery.OrderByDescending(columnsMap[sortBy]);
        }

        public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> query,
            int pageIndex = 1,
            int pageSize = 10)
        {
            if (pageIndex <= 0)
                pageIndex = 1;

            if (pageSize <= 0)
                pageSize = 10;

            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
