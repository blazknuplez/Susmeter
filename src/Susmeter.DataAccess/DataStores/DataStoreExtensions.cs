using Susmeter.Ef;
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
    }
}
