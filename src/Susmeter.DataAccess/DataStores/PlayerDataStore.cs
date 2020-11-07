using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Susmeter.Abstractions;
using Susmeter.Abstractions.Infrastructure;
using Susmeter.Abstractions.Models;
using Susmeter.Ef;
using Susmeter.Ef.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Susmeter.DataAccess.DataStores
{
    public interface IPlayerDataStore : IDataStore
    {
        Task<PlayerEntity> AddPlayerAsync(string name, string nickname, Color color, CancellationToken cancellationToken);

        Task<List<Player>> ListPlayersAsync(CancellationToken cancellationToken);

        Task<Player> FindPlayerAsync(long playerId, CancellationToken cancellationToken);

        Task UpdatePlayerAsync(Player player, CancellationToken cancellationToken);
    }

    public class PlayerDataStore : DataStore, IPlayerDataStore
    {
        public PlayerDataStore(SusmeterDbContext context, IConfigurationProvider mapper, ILogger<PlayerDataStore> logger) 
            : base(context, mapper, logger)
        {
        }

        public async Task<PlayerEntity> AddPlayerAsync(string name, string nickname, Color color, CancellationToken cancellationToken)
        {
            var colorEntity = await Context.FindEntityAsync<ColorEntity>(color.HexValue(), cancellationToken);
            var entity = new PlayerEntity { Name = name, Nickname = nickname, AvatarColor = colorEntity };
            await Context.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<List<Player>> ListPlayersAsync(CancellationToken cancellationToken)
        {
            return await Context.Set<PlayerEntity>()
                .ProjectTo<Player>(Mapper)
                .OrderBy(i => i.Nickname)
                .ToListAsync(cancellationToken);
        }

        public async Task<Player> FindPlayerAsync(long playerId, CancellationToken cancellationToken)
        {
            return await Context.Set<PlayerEntity>()
                .Where(i => i.PlayerId == playerId)
                .ProjectTo<Player>(Mapper)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdatePlayerAsync(Player player, CancellationToken cancellationToken)
        {
            var entity = await Context.FindEntityAsync<PlayerEntity>(player.PlayerId, cancellationToken);
            var colorEntity = await Context.FindEntityAsync<ColorEntity>(player.AvatarHexColor, cancellationToken);

            entity.Name = player.Name;
            entity.Nickname = player.Nickname;
            entity.AvatarColor = colorEntity;

            Context.Update(entity);
        }
    }
}
