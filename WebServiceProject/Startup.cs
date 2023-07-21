using System.Net;

namespace WebServiceProject
{
    public class Startup
    {
        private IWebHostEnvironment _env;
        public static IConfiguration StaticConfig { get; private set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            StaticConfig = configuration;

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
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(op => true));
        }
    }
}
