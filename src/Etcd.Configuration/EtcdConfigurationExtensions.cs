using Microsoft.Extensions.Configuration;

namespace Etcd.Configuration
{
    public static class EtcdConfigurationExtensions
    {
        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, IConfiguration etcdConfiguration)
        {
            var configRepository = new EtcdConfigRepository(etcdConfiguration.Get<EtcdOptions>());
            return builder.Add(new EtcdConfigurationProvider(configRepository));
        }
    }
}
