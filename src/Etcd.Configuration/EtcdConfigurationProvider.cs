using Microsoft.Extensions.Configuration;

namespace Etcd.Configuration
{
    public class EtcdConfigurationProvider : ConfigurationProvider, IConfigurationSource, IConfigrationWatcher
    {
        private readonly IConfigRepository _configRepository;

        public EtcdConfigurationProvider(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
            _configRepository.AddWatcher(this);
        }

        public override void Load()
        {
            _configRepository.Initialize();

            LoadData();
        }

        protected virtual void LoadData()
        {
            Data = _configRepository.GetConfig();
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => this;

        public void OnChange()
        {
            LoadData();
        }
    }
}
