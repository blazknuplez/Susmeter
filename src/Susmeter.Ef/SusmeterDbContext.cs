using Microsoft.EntityFrameworkCore;
using Susmeter.Ef.DataSeed;
using System.Diagnostics.CodeAnalysis;

namespace Susmeter.Ef
{
    public class SusmeterDbContext : DbContext
    {
        public SusmeterDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SusmeterDbContext).Assembly);
            
            modelBuilder
                .SeedColors()
                .SeedPlayers();
        }
    }
}
