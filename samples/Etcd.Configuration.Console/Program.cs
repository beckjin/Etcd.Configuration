using Etcd.Configuration.Console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Etcd.Configuration.Console
{
    class Program
    {
        public IConfiguration Configuration { get; }

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var testService = serviceProvider.GetService<ITestService>();

            while (true)
            {
                var key = System.Console.ReadLine();
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }
                if (key.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                {
                    Environment.Exit(0);
                }
                var val = testService.GetConfig(key);
                System.Console.WriteLine(val);
            }
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            services.AddSingleton(loggerFactory);
            services.AddLogging();

            IConfiguration configuration = GetConfiguration();
            services.AddSingleton(configuration);

            services.AddScoped<ITestService, TestService>();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true);

            builder.AddEtcd(builder.Build().GetSection("etcd"));

            return builder.Build();
        }
    }
}
