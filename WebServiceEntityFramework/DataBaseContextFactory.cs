using Microsoft.EntityFrameworkCore;

namespace WebServiceEntityFramework
{
    public class DataBaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public DataBaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public DataBaseContext CreateDbContext()
        {
            DbContextOptionsBuilder<DataBaseContext> options = new DbContextOptionsBuilder<DataBaseContext>();

            _configureDbContext(options);

            return new DataBaseContext(options.Options);
        }
    }
}
