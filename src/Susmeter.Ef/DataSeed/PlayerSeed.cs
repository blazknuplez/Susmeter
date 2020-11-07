using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susmeter.Abstractions;
using Susmeter.Abstractions.Infrastructure;
using Susmeter.Ef.Entities;

namespace Susmeter.Ef.DataSeed
{
    public static class PlayerSeed
    {
        public static ModelBuilder SeedPlayers(this ModelBuilder builder)
        {
            EntityTypeBuilder<PlayerEntity> b = builder.Entity<PlayerEntity>();

            b.HasData(Create(1, "Blaž", "not knupli", Color.Yellow));
            b.HasData(Create(2, "Lara", "Meronja", Color.Pink));

            return builder;
        }

        private static PlayerEntity Create(long id, string name, string nickname, Color color)
        {
            return new PlayerEntity { PlayerId = id, Name = name, Nickname = nickname, AvatarHexColor = color.HexValue() };
        }
    }
}
