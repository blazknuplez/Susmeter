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

        Task<List<TopImpostor>> ListTopImpostorsAsync(Filter filter, CancellationToken cancellationToken = default);

        Task<List<RoleStats>> ListDeadliestImpostorAsync(Filter filter, CancellationToken cancellationToken = default);

        Task<List<RoleStats>> List5HeadCrewmatesAsync(Filter filter, CancellationToken cancellationToken = default);

        Task<List<RoleStats>> ListWorstImpostorAsync(Filter filter, CancellationToken cancellationToken = default);
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

        public async Task<List<TopImpostor>> ListTopImpostorsAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            var data = await GetFlatDataForRole(Role.Impostor, filter, cancellationToken);

            return data.GroupBy(i => new { i.PlayerId, i.Nickname })
                .Select(i => new TopImpostor
                {
                    PlayerId = i.Key.PlayerId,
                    Nickname = i.Key.Nickname,
                    ImpostorGames = i.Count()
                })
                .SortAndTake(i => i.ImpostorGames, filter);
        }

        public async Task<List<RoleStats>> ListDeadliestImpostorAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            var data = await GetFlatDataForRole(Role.Impostor, filter, cancellationToken);
            return GroupStatsForRole(data, Role.Impostor, filter);
        }

        public async Task<List<RoleStats>> List5HeadCrewmatesAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            var data = await GetFlatDataForRole(Role.Crewmate, filter, cancellationToken);
            return GroupStatsForRole(data, Role.Crewmate, filter);
        }

        public async Task<List<RoleStats>> ListWorstImpostorAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            filter.SortOrder = SortOrder.Ascending;
            var data = await GetFlatDataForRole(Role.Impostor, filter, cancellationToken);
            return GroupStatsForRole(data, Role.Impostor, filter);
        }

        private async Task<List<RoleStatsQuery>> GetFlatDataForRole(Role playerRole, Filter filter, CancellationToken cancellationToken)
        {
            return await Context.Set<MatchPlayerEntity>()
                .Where(i => i.PlayerRole == playerRole && i.Match.Timestamp >= filter.CutOffDate)
                .Select(i => new RoleStatsQuery { PlayerId = i.PlayerId, Nickname = i.Player.Nickname, WinningRole = i.Match.Winner })
                .ToListAsync(cancellationToken);
        }

        private List<RoleStats> GroupStatsForRole(List<RoleStatsQuery> data, Role playerRole, Filter filter)
        {
            // todo check why LINQ to Entities fails for this for SQLite db
            return data.GroupBy(i => new { i.PlayerId, i.Nickname })
                .Where(i => i.Count() > filter.MinGames)
                .Select(i => new RoleStats
                {
                    PlayerId = i.Key.PlayerId,
                    Nickname = i.Key.Nickname,
                    WinPercent = (decimal)i.Count(j => j.WinningRole == playerRole) / i.Count() * 100
                })
                .SortAndTake(i => i.WinPercent, filter);
        }
    }
}
