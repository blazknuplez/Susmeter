using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susmeter.Abstractions.Infrastructure;
using Susmeter.Abstractions.Models;
using Susmeter.Ef.Entities;

namespace Susmeter.Ef.DataSeed
{
    public static class ColorSeed
    {
        public static ModelBuilder SeedColors(this ModelBuilder builder)
        {
            EntityTypeBuilder<ColorEntity> b = builder.Entity<ColorEntity>();

            foreach (var color in EnumExtensions.GetEnumValues<Color>())
            {
                b.HasData(Create(color.HexValue(), color.ToString()));
            }

            return builder;
        }

        private static ColorEntity Create(string hexCode, string colorName)
        {
            return new ColorEntity { HexCode = hexCode, ColorName = colorName };
        }
    }
}
