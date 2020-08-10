using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace Etcd.Configuration
{
    public class EtcdConfigurationProvider : ConfigurationProvider, IConfigurationSource, IConfigrationWatcher
    {
        private readonly IConfigrationRepository _configRepository;

        public EtcdConfigurationProvider(IConfigrationRepository configRepository, bool reloadOnChange, Action<IConfigurationRoot> actionOnChange)
        {
            _configRepository = configRepository;

            if (reloadOnChange || actionOnChange != null)
            {
                _configRepository.Watch(this);

                ChangeToken.OnChange(
                    () => GetReloadToken(),
                    () =>
                    {
                        Load();

                        //return the latest configuration
                        if (actionOnChange != null)
                        {
                            var builder = new ConfigurationBuilder().AddInMemoryCollection(Data).Build();
                            actionOnChange.Invoke(builder);
                        }
                    }
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
