﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Susmeter.Ef;
using System.Threading.Tasks;

namespace Susmeter.DataAccess.DataStores
{
    public interface IDataStore
    {
        Task<int> DetectAndSaveAsync();
    }

    public abstract class DataStore : IDataStore
    {
        public DataStore(SusmeterDbContext context, IConfigurationProvider mapper, ILogger logger)
        {
            Context = context;
            Mapper = mapper;
            Logger = logger;
        }

        protected SusmeterDbContext Context { get; private set; }

        protected IConfigurationProvider Mapper { get; private set; }

        protected ILogger Logger { get; private set; }

        public async Task<int> DetectAndSaveAsync()
        {
            Context.ChangeTracker.DetectChanges();
            return await Context.SaveChangesAsync();
        }
    }
}