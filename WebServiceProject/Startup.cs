using Microsoft.EntityFrameworkCore;
using WebServiceDomain.Services;
using WebServiceEntityFramework;
using WebServiceEntityFramework.Services;

namespace WebServiceProject
{
    public class Startup
    {
        private IWebHostEnvironment env;
        public IConfiguration configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.env = env;
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                });
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            var connectionString = configuration["ConnectionStrings:DatabaseConnection"];
            void ConfigureDbContext(DbContextOptionsBuilder o) => o.UseNpgsql(connectionString);
            services.AddDbContext<DataBaseContext>(ConfigureDbContext);
            services.AddSingleton( new DataBaseContextFactory(ConfigureDbContext));

            services.AddScoped<IDataService, DataService>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(op => true));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
