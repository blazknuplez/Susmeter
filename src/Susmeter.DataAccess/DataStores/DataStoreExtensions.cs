using Susmeter.Abstractions;
using Susmeter.Abstractions.Infrastructure;
using Susmeter.Ef;
using Susmeter.Ef.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Susmeter.DataAccess.DataStores
{
    public static class DataStoreExtensions
    {
        public static async Task<ColorEntity> FindColorAsync(this SusmeterDbContext context, Color color, CancellationToken cancellationToken)
        {
           return await context.FindColorAsync(color.HexValue(), cancellationToken);
        }

        public static async Task<ColorEntity> FindColorAsync(this SusmeterDbContext context, string color, CancellationToken cancellationToken)
        {
            return await context.FindAsync<ColorEntity>(color, cancellationToken);
        }
    }
}
