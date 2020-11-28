using dotnet_etcd;
using Etcdserverpb;
using Grpc.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etcd.Configuration
{
    public class EtcdConfigurationRepository : IConfigrationRepository
    {
        private readonly EtcdOptions _etcdOptions;
        private readonly EtcdClient _etcdClient;
        private readonly Metadata _headers;
        public EtcdConfigurationRepository(EtcdOptions etcdOptions)
        {
            if (string.IsNullOrEmpty(etcdOptions.ConnectionString))
            {
                throw new ArgumentNullException($"{nameof(etcdOptions.ConnectionString)} can't be null");
            }

            if (etcdOptions.PrefixKeys == null || !etcdOptions.PrefixKeys.Any())
            {
                throw new ArgumentNullException($"{nameof(etcdOptions.PrefixKeys)} can't be null");
            }

            _etcdOptions = etcdOptions;
            _etcdClient = new EtcdClient(etcdOptions.ConnectionString,
                caCert: _etcdOptions.CaCert,
                clientCert: _etcdOptions.ClientCert,
                clientKey: _etcdOptions.ClientKey,
                publicRootCa: _etcdOptions.PublicRootCa);

            if (!string.IsNullOrEmpty(_etcdOptions.Username) && !string.IsNullOrEmpty(_etcdOptions.Password))
            {
                var authRes = _etcdClient.Authenticate(new AuthenticateRequest { Name = _etcdOptions.Username, Password = _etcdOptions.Password });

                _headers = new Metadata
                {
                    { "Authorization", authRes.Token }
                };
            }
        }

        public IDictionary<string, string> GetConfig()
        {
            var dict = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var prefixKey in _etcdOptions.PrefixKeys)
            {
                var fullPrefixKey = $"{_etcdOptions.Env}{prefixKey}";

                var kvs = _etcdClient.GetRange(fullPrefixKey, _headers).Kvs;

                foreach (var item in kvs)
                {
                    var key = item.Key.ToStringUtf8();
                    var val = item.Value.ToStringUtf8();

                    if (_etcdOptions.KeyMode == EtcdConfigrationKeyMode.Json)
                    {
                        key = $"{prefixKey}:{key.Replace(fullPrefixKey, string.Empty).Replace("/", ":")}";
                    }
                    else if (_etcdOptions.KeyMode == EtcdConfigrationKeyMode.RemovePrefix)
                    {
                        key = key.Replace(fullPrefixKey, string.Empty);
                    }
                    else
                    {
                        key = key.Replace(_etcdOptions.Env, string.Empty);
                    }

                    if (dict.ContainsKey(key))
                    {
                        dict[key] = val;
                    }
                    else
                    {
                        dict.TryAdd(key, val);
                    }
                }
            }

            return dict;
        }

        public void Watch(IConfigrationWatcher watcher)
        {
            Task.Run(() =>
            {
                var keys = _etcdOptions.PrefixKeys;

                if (!string.IsNullOrEmpty(_etcdOptions.Env))
                {
                    keys = _etcdOptions.PrefixKeys.Select(prefixKey => $"{ _etcdOptions.Env }{prefixKey}").ToList();
                }

                try
                {
                    _etcdClient.WatchRange(keys.ToArray(), (WatchResponse response) =>
                    {
                        if (response.Events.Count > 0)
                        {
                            watcher.FireChange();
                        }
                    }, _headers);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            });
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
