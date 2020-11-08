using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Susmeter.DataAccess.DataStores;
using Susmeter.DataAccess.Infrastructure;
using Susmeter.Ef;

namespace Susmeter.DataAccess
{
    public static class DataAccessInstallerExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SusmeterDbContext>(opt => opt.UseSqlite("Data Source = Susmeter.db"));

            services.AddScoped<IPlayerDataStore, PlayerDataStore>();
            services.AddScoped<IMatchDataStore, MatchDataStore>();
            services.AddScoped<IStatsDataStore, StatsDataStore>();
            services.AddAutoMapper(c => c.AddProfile<DbMappingProfile>());

            return services;
        }
    }
}
