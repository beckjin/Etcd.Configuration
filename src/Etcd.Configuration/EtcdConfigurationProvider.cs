using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace Etcd.Configuration
{
    public class EtcdConfigurationProvider : ConfigurationProvider, IConfigurationSource, IConfigrationWatcher
    {
        private readonly IConfigrationRepository _configRepository;
        private readonly Action<IConfigurationRoot> _actionOnChange;

        public EtcdConfigurationProvider(IConfigrationRepository configRepository, bool reloadOnChange, Action<IConfigurationRoot> actionOnChange)
        {
            _configRepository = configRepository;
            _actionOnChange = actionOnChange;

            if (reloadOnChange || actionOnChange != null)
            {
                _configRepository.Watch(this);
            }
        }

        private void Reload()
        {
            Load();

            //return the latest configuration
            if (_actionOnChange != null)
            {
                var builder = new ConfigurationBuilder().AddInMemoryCollection(Data).Build();
                _actionOnChange.Invoke(builder);
            }
        }

        public override void Load()
        {
            Data = _configRepository.GetConfig();
        }

        public void FireChange()
        {
            Reload();
            OnReload();
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => this;
    }
}
