using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Etcd.Configuration
{
    public class EtcdConfigurationProvider : ConfigurationProvider, IConfigurationSource, IConfigrationWatcher
    {
        private readonly IConfigrationRepository _configRepository;
        private Task? _initializeTask;

        public EtcdConfigurationProvider(IConfigrationRepository configRepository)
        {
            _configRepository = configRepository;
            _configRepository.AddWatcher(this);
            _initializeTask = _configRepository.Initialize();
        }

        public override void Load()
        {
            Interlocked.Exchange(ref _initializeTask, null)?.ConfigureAwait(false).GetAwaiter().GetResult();

            SetData();
        }

        private void SetData()
        {
            Data = _configRepository.GetConfig();
        }

        public void OnChange()
        {
            SetData();

            OnReload();
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => this;
    }
}
