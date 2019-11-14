using System;
using System.Collections.Generic;

namespace Etcd.Configuration
{
    public interface IConfigrationRepository : IDisposable
    {
        IDictionary<string, string> GetConfig();

        void Watch(IConfigrationWatcher watcher);
    }
}
