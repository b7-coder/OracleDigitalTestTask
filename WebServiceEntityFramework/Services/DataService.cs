using Microsoft.Extensions.Logging;
using WebServiceDomain.Models;
using WebServiceDomain.Services;

namespace WebServiceEntityFramework.Services
{
    public class DataService : IDataService
    {
        private readonly DataBaseContextFactory dbFactory;
        private readonly ILogger<DataService> logger;

        public DataService(DataBaseContextFactory dbFactory, ILogger<DataService> logger)
        {
            this.dbFactory = dbFactory;
            this.logger = logger;
        }
        public async Task<bool> Create(string key, string value)
        {
            DataModel entity = new DataModel { Jsonkey = key, Jsonvalue = value };

            try
            {
                var dbContext = dbFactory.CreateDbContext();

                await dbContext.DataModels.AddAsync(entity);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex) 
            {
                logger.LogError(ex.Message);

                return false;
            }
        }
    }
}
