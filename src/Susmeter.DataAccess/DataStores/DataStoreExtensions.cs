using Susmeter.Abstractions.Models;
using Susmeter.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Susmeter.DataAccess.DataStores
{
    public static class DataStoreExtensions
    {
        public static async ValueTask<TEntity> FindEntityAsync<TEntity>(this SusmeterDbContext context, object key, CancellationToken cancellationToken)
            where TEntity : class
        {
            return await context.Set<TEntity>().FindAsync(new object[] { key }, cancellationToken);
        }

        public static List<T> SortAndTake<T>(this IEnumerable<T> items, Func<T, decimal> func, Filter filter)
        {
            if (filter.SortOrder == SortOrder.Ascending)
                return items.OrderBy(func).Take(filter.Page * filter.PageSize).ToList();

            return items.OrderByDescending(func).Take(filter.Page * filter.PageSize).ToList();
        }
    }
}
