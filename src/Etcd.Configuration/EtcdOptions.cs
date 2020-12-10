using System.Collections.Generic;

namespace Etcd.Configuration
{
    /// <summary>
    /// Etcd Setting Options
    /// </summary>
    public class EtcdOptions
    {
        /// <summary>
        /// Etcd Connection String
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Timeout [in milliseconds] after disconnection to wait before try watch keys again.
        /// </summary>
        public int RewatchTimeoutInMs { get; set; } = 10000;

        /// <summary>
        /// Environment. /dev or /uat or /prod  Default : Empty String
        /// </summary>
        public string Env { get; set; } = string.Empty;

        /// <summary>
        /// config prefixKeys, no need to include env value
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
        /// Set key mode. Default : Json
        /// </summary>
        public EtcdConfigrationKeyMode KeyMode { get; set; } = EtcdConfigrationKeyMode.Json;

    }
}
