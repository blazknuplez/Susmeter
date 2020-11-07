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
            b.HasData(Create(3, "Tancer", "c00kie", Color.Lime));
            b.HasData(Create(4, "Roki", "thefk69", Color.Brown));
            b.HasData(Create(5, "Kevin", "Krušni oče", Color.Black));
            b.HasData(Create(6, "Eagle", "Eagle", Color.White));
            b.HasData(Create(7, "Dani", "Bani", Color.Blue));
            b.HasData(Create(8, "Valerija", "Val", Color.Cyan));
            b.HasData(Create(9, "Jure", "Jure", Color.Green));
            b.HasData(Create(10, "Arijan", "Archie", Color.Purple));

            return builder;
        }

        private static PlayerEntity Create(long id, string name, string nickname, Color color)
        {
            return new PlayerEntity { PlayerId = id, Name = name, Nickname = nickname, AvatarHexColor = color.HexValue() };
        }
    }
}
