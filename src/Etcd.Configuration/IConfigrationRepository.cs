using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etcd.Configuration
{
    public interface IConfigrationRepository : IDisposable
    {
        Task Initialize();

        IDictionary<string, string> GetConfig();

        void AddWatcher(IConfigrationWatcher watcher);

        void RemoveWatcher(IConfigrationWatcher watcher);
    }
}
