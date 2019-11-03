using System;
using System.Collections.Concurrent;

namespace Etcd.Configuration
{
    public interface IConfigrationRepository : IDisposable
    {
        void Initialize();

        ConcurrentDictionary<string, string> GetConfig();

        void AddWatcher(IConfigrationWatcher watcher);

        void RemoveWatcher(IConfigrationWatcher watcher);
    }
}
