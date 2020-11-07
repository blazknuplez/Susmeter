using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susmeter.Ef.Entities;
using Susmeter.Ef.Infrastructure;

namespace Susmeter.Ef.Configuration
{
    public class MatchPlayerConfiguration : IEntityTypeConfiguration<MatchPlayerEntity>
    {
        public void Configure(EntityTypeBuilder<MatchPlayerEntity> builder)
        {
            builder.ToTable(nameof(MatchPlayerEntity).TableName());
            builder.HasKey(x => x.MatchPlayerId);
            builder.Property(x => x.MatchPlayerId).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Match)
                .WithMany(x => x.Players)
                .HasForeignKey(i => i.MatchId)
                .HasPrincipalKey(i => i.MatchId);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.Matches)
                .HasForeignKey(i => i.PlayerId)
                .HasPrincipalKey(i => i.PlayerId);

            builder.HasOne(i => i.Color)
                .WithMany()
                .HasForeignKey(i => i.HexColor)
                .HasPrincipalKey(i => i.HexCode);
        }
    }
}
