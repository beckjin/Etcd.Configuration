using Microsoft.Extensions.Configuration;
using System;

namespace Etcd.Configuration
{
    public static class EtcdConfigurationExtensions
    {
        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, IConfiguration etcdConfiguration, bool reloadOnChange = false, Action<IConfigurationRoot> actionOnChange = null)
        {
            var configRepository = new EtcdConfigurationRepository(etcdConfiguration.Get<EtcdOptions>());
            return builder.Add(new EtcdConfigurationProvider(configRepository, reloadOnChange, actionOnChange));
        }
    }
}
