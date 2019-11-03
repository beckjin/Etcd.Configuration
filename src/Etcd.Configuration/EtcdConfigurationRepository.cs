using dotnet_etcd;
using Etcdserverpb;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Etcd.Configuration
{
    public class EtcdConfigurationRepository : IConfigrationRepository
    {
        private readonly EtcdOptions _etcdOptions;
        private readonly EtcdClient _etcdClient;

        private readonly List<IConfigrationWatcher> _watchers = new List<IConfigrationWatcher>();

        public EtcdConfigurationRepository(EtcdOptions etcdOptions)
        {
            if (etcdOptions.Hosts == null || !etcdOptions.Hosts.Any())
            {
                throw new ArgumentNullException("etcd hosts can't be null");
            }

            if (etcdOptions.PrefixKeys == null || !etcdOptions.PrefixKeys.Any())
            {
                throw new ArgumentNullException("etcd prefixKeys can't be null");
            }

            _etcdOptions = etcdOptions;
            _etcdClient = new EtcdClient(string.Join(",", _etcdOptions.Hosts), 
                username: _etcdOptions.Username, 
                password: _etcdOptions.Password, 
                caCert: _etcdOptions.CaCert, 
                clientCert: _etcdOptions.ClientCert, 
                clientKey: _etcdOptions.ClientKey,
                publicRootCa: _etcdOptions.PublicRootCa);
        }

        public void Initialize()
        {
            // watching 
            _etcdClient.WatchRange(_etcdOptions.PrefixKeys.ToArray(), (WatchResponse response) =>
            {
                if (response.Events.Count > 0)
                {
                    FireChange();
                }
            });
        }

        public ConcurrentDictionary<string, string> GetConfig()
        {
            var configs = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var prefixKey in _etcdOptions.PrefixKeys)
            {
                var kvs = _etcdClient.GetRange(prefixKey).Kvs;
                foreach (var item in kvs)
                {
                    var key = item.Key.ToStringUtf8().Replace(prefixKey, string.Empty);
                    var val = item.Value.ToStringUtf8();
                    if (configs.ContainsKey(key))
                    {
                        configs[key] = val;
                    }
                    else
                    {
                        configs.TryAdd(key, val);
                    }
                }
            }

            return configs;
        }

        public void AddWatcher(IConfigrationWatcher watcher)
        {
            lock (_watchers)
                if (!_watchers.Contains(watcher))
                {
                    _watchers.Add(watcher);
                }
        }

        public void RemoveWatcher(IConfigrationWatcher watcher)
        {
            lock (_watchers)
                _watchers.Remove(watcher);
        }

        protected void FireChange()
        {
            lock (_watchers)
                foreach (var watcher in _watchers)
                {
                    watcher.OnChange();
                }
        }

        #region Dispose

        bool _disposed;
        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _etcdClient.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EtcdConfigurationRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}
