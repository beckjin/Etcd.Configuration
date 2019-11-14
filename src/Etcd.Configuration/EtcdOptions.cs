using System.Collections.Generic;

namespace Etcd.Configuration
{
    /// <summary>
    /// Etcd Setting Options
    /// </summary>
    public class EtcdOptions
    {
        /// <summary>
        /// Etcd hosts
        /// </summary>
        public List<string> Hosts { get; set; }

        /// <summary>
        /// config prefixKeys
        /// </summary>
        public List<string> PrefixKeys { get; set; }

        /// <summary>
        /// String containing username for etcd basic auth. Default : Empty String
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// String containing password for etcd basic auth. Default : Empty String
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// String containing ca cert when using self signed certificates with etcd. Default : Empty String
        /// </summary>
        public string CaCert { get; set; } = string.Empty;

        /// <summary>
        /// String containing client cert when using self signed certificates with client auth enabled in etcd. Default : Empty String
        /// </summary>
        public string ClientCert { get; set; } = string.Empty;

        /// <summary>
        /// String containing client key when using self signed certificates with client auth enabled in etcd. Default : Empty String
        /// </summary>
        public string ClientKey { get; set; } = string.Empty;

        /// <summary>
        /// Bool depicting whether to use publicy trusted roots to connect to etcd. Default : false.
        /// </summary>
        public bool PublicRootCa { get; set; }

        /// <summary>
        /// Set config key mode
        /// </summary>
        public EtcdConfigrationKeyMode KeyMode { get; set; }
    }
}
