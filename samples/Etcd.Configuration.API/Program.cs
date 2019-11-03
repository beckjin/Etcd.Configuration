using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Etcd.Configuration.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(builder =>
                    {
                        builder.AddEtcd(builder.Build().GetSection("etcd"));
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
