using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Susmeter.Abstractions.Models;
using Susmeter.Ef;
using Susmeter.Ef.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Susmeter.DataAccess.DataStores
{
    public interface IStatsDataStore : IDataStore
    {
        Task<MatchStatTotals> MatchStatTotalsAsync(CancellationToken cancellationToken = default);

        Task<List<TopImpostor>> ListTopImpostorsAsync(CancellationToken cancellationToken = default);
    }

    public class StatsDataStore : DataStore, IStatsDataStore
    {
        public StatsDataStore(SusmeterDbContext context, IConfigurationProvider mapper, ILogger<StatsDataStore> logger) 
            : base(context, mapper, logger)
        {
        }

        public async Task<MatchStatTotals> MatchStatTotalsAsync(CancellationToken cancellationToken = default)
        {
            var set = Context.Set<MatchEntity>();

            return new MatchStatTotals
            {
                MatchesPlayed = await set.CountAsync(cancellationToken),
                CrewmateWins = await set.CountAsync(i => i.Winner == Role.Crewmate, cancellationToken),
                ImpostorWins = await set.CountAsync(i => i.Winner == Role.Impostor, cancellationToken),
            };
        }

        public async Task<List<TopImpostor>> ListTopImpostorsAsync(CancellationToken cancellationToken = default)
        {
            var data = await Context.Set<MatchPlayerEntity>()
                .Where(i => i.PlayerRole == Role.Impostor)
                .Select(i => new { i.PlayerId, i.Player.Nickname })
                .ToListAsync(cancellationToken);

            return data.GroupBy(i => new { i.PlayerId, i.Nickname })
                .Select(i => new TopImpostor
                {
                    PlayerId = i.Key.PlayerId,
                    Nickname = i.Key.Nickname,
                    ImpostorGames = i.Count()
                })
                .OrderByDescending(i => i.ImpostorGames)
                .Take(3)
                .ToList();
        }

    }
}
