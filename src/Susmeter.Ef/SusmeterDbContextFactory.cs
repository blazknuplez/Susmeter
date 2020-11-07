using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Susmeter.Ef
{
    public class SusmeterDbContextFactory : IDesignTimeDbContextFactory<SusmeterDbContext>
    {
        public SusmeterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SusmeterDbContext>();
            optionsBuilder.UseSqlite("FileName=../Susmeter.db");

            return new SusmeterDbContext(optionsBuilder.Options);
        }
    }
}
