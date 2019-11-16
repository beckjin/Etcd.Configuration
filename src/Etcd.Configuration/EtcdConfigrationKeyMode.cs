namespace Etcd.Configuration
{
    /// <summary>
    /// Key Modes
    /// </summary>
    public enum EtcdConfigrationKeyMode
    {
        /// <summary>
        /// key: /a/b/c
        /// prefixKey "/a/b/" => /a/b/c
        /// </summary>
        Default = 0,

        /// <summary>
        /// key: /a/b/c
        /// prefixKey "/a/b/" => /a/b/:c
        /// </summary>
        Json = 1,

        /// <summary>
        /// key: /a/b/c
        /// prefixKey "/a/b/" => c
        /// </summary>
        RemovePrefix = 2
    }
}
