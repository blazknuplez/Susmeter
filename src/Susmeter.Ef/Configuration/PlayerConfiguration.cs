using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susmeter.Ef.Entities;
using Susmeter.Ef.Infrastructure;

namespace Susmeter.Ef.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<PlayerEntity>
    {
        public void Configure(EntityTypeBuilder<PlayerEntity> builder)
        {
            builder.ToTable(nameof(PlayerEntity).TableName());
            builder.HasKey(x => x.PlayerId);
            builder.Property(x => x.PlayerId).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Nickname).IsRequired().HasMaxLength(25);

            builder.HasOne(i => i.AvatarColor)
                .WithMany()
                .HasForeignKey(i => i.AvatarHexColor)
                .HasPrincipalKey(i => i.HexCode);
        }
    }
}
