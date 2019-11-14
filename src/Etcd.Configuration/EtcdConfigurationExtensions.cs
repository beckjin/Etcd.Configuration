using Microsoft.Extensions.Configuration;

namespace Etcd.Configuration
{
    public static class EtcdConfigurationExtensions
    {
        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, IConfiguration etcdConfiguration, bool reloadOnChange = false)
        {
            var configRepository = new EtcdConfigurationRepository(etcdConfiguration.Get<EtcdOptions>());
            return builder.Add(new EtcdConfigurationProvider(configRepository, reloadOnChange));
        }
    }
}
