﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public interface IMatchDataStore : IDataStore
    {
        Task<MatchEntity> AddMatchAsync(Match match, CancellationToken cancellationToken = default);

        Task<List<Match>> ListMatchesAsync(CancellationToken cancellationToken = default);

        Task<Match> FindMatchAsync(long matchId, CancellationToken cancellationToken = default);
    }

    public class MatchDataStore : DataStore, IMatchDataStore
    {
        public MatchDataStore(SusmeterDbContext context, IConfigurationProvider mapper, ILogger<PlayerDataStore> logger) 
            : base(context, mapper, logger)
        {
        }

        public async Task<MatchEntity> AddMatchAsync(Match match, CancellationToken cancellationToken = default)
        {
            var entity = new MatchEntity { Timestamp = match.Timestamp, Winner = match.Winner };
            match.Players.ForEach(async p => await AddPlayerToMatch(entity, p, cancellationToken));
            await Context.AddAsync(entity);
            return entity;
        }

        public async Task<List<Match>> ListMatchesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<MatchEntity>()
                .OrderByDescending(i => i.MatchId)
                .Take(50)
                .ProjectTo<Match>(Mapper)
                .ToListAsync(cancellationToken);
        }

        public async Task<Match> FindMatchAsync(long matchId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<MatchEntity>()
                .Where(i => i.MatchId == matchId)
                .ProjectTo<Match>(Mapper)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private async Task AddPlayerToMatch(MatchEntity entity, MatchPlayer player, CancellationToken cancellationToken)
        {
            var playerEntity = await Context.FindEntityAsync<PlayerEntity>(player.PlayerId, cancellationToken);
            var colorEntity = await Context.FindEntityAsync<ColorEntity>(player.HexColor, cancellationToken);

            entity.Players.Add(new MatchPlayerEntity
            {
                Player = playerEntity,
                Color = colorEntity,
                PlayerRole = player.PlayerRole
            });
        }
    }
}
