using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susmeter.Ef.Entities;
using Susmeter.Ef.Infrastructure;

namespace Susmeter.Ef.Configuration
{
    public class ColorConfiguration : IEntityTypeConfiguration<ColorEntity>
    {
        public void Configure(EntityTypeBuilder<ColorEntity> builder)
        {
            builder.ToTable(nameof(ColorEntity).TableName());
            builder.HasKey(x => x.HexCode);
            builder.Property(x => x.HexCode).IsRequired().HasMaxLength(7);
            builder.Property(x => x.ColorName).IsRequired().HasMaxLength(25);
        }
    }
}
