using WebServiceProject;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); 
                    //Urls(webBuilder);
                });

static void Urls(IWebHostBuilder webBuilder)
{
    webBuilder.UseUrls("http://0.0.0.0:3005/", "https://0.0.0.0:3006/");
}