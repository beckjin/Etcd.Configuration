namespace Etcd.Configuration
{
    public interface IConfigrationWatcher
    {
        void OnChange();
    }
}
