using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susmeter.Ef.Entities;
using Susmeter.Ef.Infrastructure;

namespace Susmeter.Ef.Configuration
{
    public class MatchConfiguration : IEntityTypeConfiguration<MatchEntity>
    {
        public void Configure(EntityTypeBuilder<MatchEntity> builder)
        {
            builder.ToTable(nameof(MatchEntity).TableName());
            builder.HasKey(x => x.MatchId);
            builder.Property(x => x.MatchId).ValueGeneratedOnAdd();
        }
    }
}
