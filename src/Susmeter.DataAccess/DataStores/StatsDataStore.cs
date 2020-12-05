using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Susmeter.Abstractions.Models;
using Susmeter.DataAccess.Models;
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

        Task<List<RoleStats>> ListDeadliestImpostorAsync(CancellationToken cancellationToken = default);

        Task<List<RoleStats>> List5HeadCrewmatesAsync(CancellationToken cancellationToken = default);
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
            var data = await GetFlatDataForRole(Role.Impostor, cancellationToken);

            return data.GroupBy(i => new { i.PlayerId, i.Nickname })
                .Where(i => i.Count() > 20)
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

        public async Task<List<RoleStats>> ListDeadliestImpostorAsync(CancellationToken cancellationToken = default)
        {
            var data = await GetFlatDataForRole(Role.Impostor, cancellationToken);
            return GroupByStatsForWinningRole(data, Role.Impostor);
        }

        public async Task<List<RoleStats>> List5HeadCrewmatesAsync(CancellationToken cancellationToken = default)
        {
            var data = await GetFlatDataForRole(Role.Crewmate, cancellationToken);
            return GroupByStatsForWinningRole(data, Role.Crewmate);
        }

        private async Task<List<RoleStatsQuery>> GetFlatDataForRole(Role playerRole, CancellationToken cancellationToken)
        {
            return await Context.Set<MatchPlayerEntity>()
                .Where(i => i.PlayerRole == playerRole)
                .Select(i => new RoleStatsQuery { PlayerId = i.PlayerId, Nickname = i.Player.Nickname, WinningRole = i.Match.Winner })
                .ToListAsync(cancellationToken);
        }

        private List<RoleStats> GroupByStatsForWinningRole(List<RoleStatsQuery> data, Role playerRole)
        {
            // todo check why LINQ to Entities fails for this for SQLite db
            return data.GroupBy(i => new { i.PlayerId, i.Nickname })
                .Where(i => i.Count() > 20)
                .Select(i => new RoleStats
                {
                    PlayerId = i.Key.PlayerId,
                    Nickname = i.Key.Nickname,
                    WinPercent = (decimal)i.Count(j => j.WinningRole == playerRole) / i.Count() * 100
                })
                .OrderByDescending(i => i.WinPercent)
                .Take(3)
                .ToList();
        }
    }
}
