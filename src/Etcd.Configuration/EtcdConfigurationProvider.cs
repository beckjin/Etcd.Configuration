using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Etcd.Configuration
{
    public class EtcdConfigurationProvider : ConfigurationProvider, IConfigurationSource, IConfigrationWatcher
    {
        private readonly IConfigrationRepository _configRepository;

        public EtcdConfigurationProvider(IConfigrationRepository configRepository, bool reloadOnChange)
        {
            _configRepository = configRepository;

            if (reloadOnChange)
            {
                _configRepository.Watch(this);

                ChangeToken.OnChange(
                    () => GetReloadToken(),
                    () => Load()
                );
            }
        }

        public override void Load()
        {
            Data = _configRepository.GetConfig();
        }

        public void FireChange() => OnReload();

        public IConfigurationProvider Build(IConfigurationBuilder builder) => this;
    }
}
