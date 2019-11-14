namespace Etcd.Configuration
{
    /// <summary>
    /// Key Modes
    /// </summary>
    public enum EtcdConfigrationKeyMode
    {
        /// <summary>
        /// prefixKey "/a/b/" => /a/b/c
        /// </summary>
        Default = 0,

        /// <summary>
        /// prefixKey "/a/b/" => /a/b/:c
        /// </summary>
        Json = 1,
    }
}
