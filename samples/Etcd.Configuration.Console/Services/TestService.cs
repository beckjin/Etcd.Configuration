using Microsoft.Extensions.Configuration;

namespace Etcd.Configuration.Console.Services
{
    public interface ITestService
    {
        string GetConfig(string key);
    }

    public class TestService : ITestService
    {
        private readonly IConfiguration _configuration;

        public TestService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConfig(string key)
        {
            return _configuration[key];
        }
    }

}
